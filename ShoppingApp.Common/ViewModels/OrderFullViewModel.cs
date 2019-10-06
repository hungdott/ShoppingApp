using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingApp.Common.ViewModels;

namespace ShoppingApp.Common.ViewModels
{
    public class OrderFullViewModel
    {
        public DateTime? CreatedDate { set; get; }
        public Order Order { get; set; }
        public IEnumerable<OrderDetail> ListOrderDetail { get; set; }
    }
}
