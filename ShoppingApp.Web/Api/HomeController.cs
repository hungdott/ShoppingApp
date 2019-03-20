﻿using ShoppingApp.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShoppingApp.Service;

namespace ShoppingApp.Web.Api
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiControllerBase
    {
      IErrorService _errorService;
      public HomeController(IErrorService errorService):base(errorService)
      {
      this._errorService = errorService;
      }
      
      [HttpGet]
    [Authorize(Roles = "Admin")]
      [Route("TestMethod")]
        
        public string TestMethod()
      {
      return "hello member";
      }
    }
}
