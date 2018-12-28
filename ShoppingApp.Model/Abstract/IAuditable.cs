using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Model.Abstract
{
  public interface IAuditable
  {

    DateTime? CreatedDate { set; get; }

    
    string CreatedBy { set; get; }

    DateTime? UpdatedDate { set; get; }

    [MaxLength(256)]
    string UpdatedBy { set; get; }

    string MetaKeyword { get; set; }
    string MetaDescription { get; set; }

    bool Status { get; set; }
  }
}
