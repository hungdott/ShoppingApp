﻿namespace ShoppingApp.Data.Migrations
{
  using Microsoft.AspNet.Identity;
  using Microsoft.AspNet.Identity.EntityFramework;
  using Model.Models;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;
  using ShoppingApp.Common;

  internal sealed class Configuration : DbMigrationsConfiguration<ShoppingApp.Data.ShoppingAppDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(ShoppingApp.Data.ShoppingAppDbContext context)
    {
      CreateProductCategorySample(context);
      //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ShoppingAppDbContext()));
      //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ShoppingAppDbContext()));
      //var user = new ApplicationUser()
      //{
      //  UserName = "hungdo",
      //  Email = "hungdo@gmail.com",
      //  EmailConfirmed = true,
      //  BirthDay = DateTime.Now,
      //  FullName = "hung do"
      //};

      //manager.Create(user, "12345$");
      //if (!roleManager.Roles.Any())
      //{
      //  roleManager.Create(new IdentityRole { Name = "Admin" });
      //  roleManager.Create(new IdentityRole { Name = "User" });
      //}
      //var adminUser = manager.FindByEmail("hungdo@gmail.com");
      //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
    }
    private void CreateProductCategorySample(ShoppingAppDbContext context)
    {
      
      if (context.ProductCategories.Count() == 0)
      {
      List<ProductCategory> listProductCategory = new List<ProductCategory>()
      {
        new ProductCategory() {Name="Điện lạnh",Alias="dien-lanh",Status=true },
        new ProductCategory() {Name="Đồ gia dụng",Alias="do-gia-dung",Status=true },
        new ProductCategory() {Name="Giải trí",Alias="giai-tri",Status=true },
        new ProductCategory() {Name="Mỹ phẩm",Alias="my-pham",Status=true }

      };
      context.ProductCategories.AddRange(listProductCategory);
      context.SaveChanges();
      }
    }
    private void CreateFooter(ShoppingAppDbContext context)
    {
      if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId)==0)
      {
        string content = "";
      }
    }
  }
}
