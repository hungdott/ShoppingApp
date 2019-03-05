using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingApp.Web.Models
{
  public class FooterViewModel
  {
    public string ID { set; get; }
    [Required]
    public string Content { set; get; }
  }
}