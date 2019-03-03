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
      CreatePage(context);
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

    private void CreatePage(ShoppingAppDbContext context)
    {
      if (context.Pages.Count() == 0)
      {
        var page = new Page()
        {
          Name="Giới thiệu",
          Alias = "gioi-thieu",
          Content = @"Ra đời từ tháng 3/2015, với sứ mệnh trở thành nhà bán lẻ các sản phẩm công nghệ và điện máy hàng đầu Việt Nam, VinPro là thương hiệu chủ lực của công ty Cổ phần Dịch vụ Thương mại Tổng hợp Vincommerce – đơn vị ngành hàng bán lẻ của Tập đoàn Vingroup.

          Với mục tiêu dẫn đầu xu hướng về công nghệ, hệ thống hàng Công nghệ - Điện máy Vinpro bao gồm chuỗi Trung tâm Công nghệ & Điện máy VinPro, nằm tại tất cả các trung tâm thương mại thuộc hệ thống Vincom. Với mô hình chuyên biệt, VinPro đáp ứng tối đa nhu cầu của khách hàng, vừa mang lại những trải nghiệm mới lạ, thú vị.

          Nhận được sự hợp tác và hỗ trợ của tất cả các thương hiệu công nghệ - điện máy hàng đầu trên thế giới, 7 ngành hàng Điện thoại - Máy tính bảng - Máy tính xách tay - Điện tử - Điện lạnh - Điện gia dụng và Phụ kiện tại VinPro đảm bảo được cập nhật xu hướng và công nghệ mới nhất, với chất lượng chính hãng và giá cả hợp lý.

          Hệ thống các Trung tâm VinPro liên tục được mở rộng trên khắp các vùng miền trong cả nước. Mục tiêu của VinPro đó là mang thế giới công nghệ thông minh tới từng người Việt, từng mái nhà Việt, giới thiệu các siêu phẩm công nghệ mới từ các thương hiệu công nghệ nổi tiếng và được ưa chuộng nhất trên thế giới.",
          Status=true
        };
        context.Pages.Add(page);
        context.SaveChanges();
      }
    }

  }
}
