using ShoppingApp.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingApp.Web.Models
{
    public class OTPString
    {
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string OTP { set; get; }



    }
}