using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ShoppingApp.Common;
using ShoppingApp.Data;
using ShoppingApp.Model.Models;
using ShoppingApp.Web.App_Start;
using ShoppingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    //IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    //authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    //ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    //AuthenticationProperties props = new AuthenticationProperties();
                    //props.IsPersistent = model.RememberMe;
                    //authenticationManager.SignIn(props, identity);
                    Session["user"] = model.UserName;
                    Session["pass"] = model.Password;

                    string num = "123456789";
                    int len = num.Length;
                    string otp = string.Empty;
                    int otpdigit = 6;
                    string finaldigit;
                    int getIndex;
                    for (int i = 0; i < otpdigit; i++)
                    {
                        do
                        {
                            getIndex = new Random().Next(0, len);
                            finaldigit = num.ToCharArray()[getIndex].ToString();
                        } while (otp.IndexOf(finaldigit)!=-1);
                        otp += finaldigit;
                    }
                    Session["otp"] = Base64Encode(otp);
                   
                    string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/OtpCode.html"));
                    content = content.Replace("{{OTP}}",otp+"SV");
     


                    MailHelper.SendMail(user.Email, "Mã OTP ShopVui", content);

                    //if (Url.IsLocalUrl(returnUrl))
                    //{
                    //    return Redirect(returnUrl);
                    //}
                    //else
                    //{
                    //    return RedirectToAction("Index", "Home");
                    //}
                    return RedirectToAction("OTP", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }

            }
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

        [HttpGet]
        public ActionResult OTP()
        {
            if (Session["otp"] == null)
            {
                return RedirectToAction("Register", "Account"); ;
                     
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> OTP(OTPString model)
        {
            var otpBase64 = Session["otp"];
            var decode = Base64Decode(otpBase64.ToString());
            if (Session["otp"] == null)
            {
                return RedirectToAction("Register", "Account");
            }
            if (Session["otp"] != null &&(decode + "SV" == model.OTP))
            {
                ApplicationUser user = _userManager.Find(Session["user"].ToString(), Session["pass"].ToString());
                IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationProperties props = new AuthenticationProperties();
                //props.IsPersistent = model.RememberMe;
                authenticationManager.SignIn(props, identity);
                Session["otp"] = null;
                Session["user"] = null;
                Session["pass"] = null;
                return RedirectToAction("Index", "Home");
                

            }
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
