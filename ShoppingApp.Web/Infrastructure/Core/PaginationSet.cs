﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingApp.Web.Infrastructure.Core
{
  public class PaginationSet<T>
  {
    public int Page { set; get; }
    public int Count
    {
      get
      {
        return (Item != null) ? Item.Count() : 0;
      }
    }
    public int TotalPages { set; get; }
    public int TotalCount { set; get; }
    public IEnumerable<T> Item { set; get; }

  }
}