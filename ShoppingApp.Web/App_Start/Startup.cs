using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Autofac;
using System.Reflection;
using Autofac.Integration.Mvc;
using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Data;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Service;
using System.Web.Mvc;
using System.Web.Http;
using Autofac.Integration.WebApi;

[assembly: OwinStartup(typeof(ShoppingApp.Web.App_Start.Startup))]

namespace ShoppingApp.Web.App_Start
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      ConfigAutofac(app);
    }
    private void ConfigAutofac(IAppBuilder app)
    {
      var builder = new ContainerBuilder();
      builder.RegisterControllers(Assembly.GetExecutingAssembly());

      //Register web api controller
      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

      builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
      builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

      builder.RegisterType<ShoppingAppDbContext>().AsSelf().InstancePerRequest();

      //repository
      builder.RegisterAssemblyTypes(typeof(PostCategoryRepository).Assembly)
        .Where(n => n.Name.EndsWith("Repository"))
        .AsImplementedInterfaces().InstancePerRequest();

      //service
      builder.RegisterAssemblyTypes(typeof(PostCategoryService).Assembly)
        .Where(n => n.Name.EndsWith("Service"))
        .AsImplementedInterfaces().InstancePerRequest();

      Autofac.IContainer container = builder.Build();
      DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

      GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
    }
  }
}
