using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingApp.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="bạn cấn nhập tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "bạn cấn nhập tên đăng nhập")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "bạn cấn nhập mật khẩu")]
        [MinLength(6,ErrorMessage ="mật khấu co ít nhất 6 ký tự")]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "bạn cấn nhập email")]
        [EmailAddress(ErrorMessage ="email không đúng")]
        public string Email { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "bạn cấn nhập số điện thoại")]
        public string PhoneNumber { get; set; } 
    }
}