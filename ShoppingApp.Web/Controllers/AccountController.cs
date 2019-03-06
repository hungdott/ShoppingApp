using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ShoppingApp.Common;
using ShoppingApp.Data;
using ShoppingApp.Model.Models;
using ShoppingApp.Web.App_Start;
using ShoppingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApp.Web.Controllers
{

    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var userByUserEmail= await _userManager.FindByEmailAsync(model.Email);
                if (userByUserEmail != null)
                {
                    ModelState.AddModelError("email","Email đã tồn tại");
                    return View(model);
                }

                var userByUserName = await _userManager.FindByNameAsync(model.UserName);
                if (userByUserName != null)
                {
                    ModelState.AddModelError("email", "Taif khoản đã tồn tại");
                    return View(model);
                }
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email =model.Email,
                    EmailConfirmed = true,
                    
                    FullName = model.FullName,
                    Address=model.Address,


                };

               await _userManager.CreateAsync(user, model.PassWord);
            


               var adminUser = await _userManager.FindByEmailAsync(model.Email);
               if (adminUser != null)
                {
                    await _userManager.AddToRolesAsync(adminUser.Id, new string[] {"User" });
                    string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/newuser.html"));
                    content = content.Replace("{{UserName}}", adminUser.FullName);
                    content = content.Replace("{{Link}}",ConfigHelper.GetByKey("CurrentLink")+"dang-nhap.html");
                
                  
                    MailHelper.SendMail(adminUser.Email, "Đăng ký thành công", content);
                    ViewData["SuccessMsg"] = "Đăng ký thành công";
                }

                
            }
                return View();
            
        }
    }
}
