using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingApp.Web.Models
{
  public class HomeViewModel
  {
    public IEnumerable<SlideViewModel> Slides { get; set; }
    public IEnumerable<ProductViewModel> LastestProducts { set; get; }
    public IEnumerable<ProductViewModel> TopSaleProducts { set; get; }
  }
}