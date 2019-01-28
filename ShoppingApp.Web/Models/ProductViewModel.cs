using ShoppingApp.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingApp.Web.Models
{
  public class ProductViewModel
  {

    public int ID { set; get; }

    [Required(ErrorMessage ="moi dien vao truong nay")]
    public string Name { set; get; }

    [Required(ErrorMessage ="moi dien vao truong nay")]
    public string Alias { set; get; }

    public int CategoryID { set; get; }

    public string Image { set; get; }

    public string MoreImages { set; get; }

    public decimal Price { set; get; }

    public decimal? PromotionPrice { set; get; }
    public int? Warranty { set; get; }

    public string Description { set; get; }
    public string Content { set; get; }

    public bool? HomeFlag { set; get; }
    public bool? HotFlag { set; get; }
    public int? ViewCount { set; get; }

    public virtual ProductCategoryViewModel ProductCategory { set; get; }
  }
}