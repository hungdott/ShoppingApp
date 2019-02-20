namespace ShoppingApp.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ShoppingApp.Data.ShoppingAppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ShoppingApp.Data.ShoppingAppDbContext context)
        {
            // This method will be called after migrating to the latest version.
            CreateUser(context);
            CreateSlide(context);
         
        }
        void CreateUser(ShoppingAppDbContext context)
        {
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ShoppingAppDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ShoppingAppDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "hungdo",
            //    Email = "hungdo99tt@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "do dang hung"

            //};

            //manager.Create(user, "123456$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("hungdo99tt@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
        private void CreateFooter(ShoppingAppDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId) == 0)
            {
              
            }
        }
        private void CreateSlide(ShoppingAppDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() {
                        Name ="Slide 1",
                        DisplayOrder =1,
                        Status =true,
                        Url ="#",
                        Image ="/Assets/client/images/bag.jpg",
                        Content =@"	<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur 
                            adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                        <span class=""on-get"">GET NOW</span>" },
                    new Slide() {
                        Name ="Slide 2",
                        DisplayOrder =2,
                        Status =true,
                        Url ="#",
                        Image ="/Assets/client/images/bag1.jpg",
                    Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>

                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >

                                <span class=""on-get"">GET NOW</span>"},
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }
    }
}
