using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingApp.Web.Models
{
  public class ContactDetailViewModel
  {
  
    public int ID { get; set; }
    [StringLength(250, ErrorMessage = "không vượt quá 250 ký tự")]
    [Required]
    public string Name { get; set; }
    [StringLength(50,ErrorMessage ="không vượt quá 50 ký tự")]
    public string Phone { get; set; }
    [StringLength(250, ErrorMessage = "không vượt quá 250 ký tự")]
    public string Email { get; set; }
    [StringLength(250, ErrorMessage = "không vượt quá 250 ký tự")]
    public string WebSite { get; set; }
    [StringLength(250, ErrorMessage = "không vượt quá 250 ký tự")]
    public string Address { get; set; }
    public string Other { get; set; }

    public double? Lat { set; get; }

    public double? Lng { set; get; }
    public bool Status { get; set; }
  }
}