using AutoMapper;
using ShoppingApp.Model.Models;
using ShoppingApp.Service;
using ShoppingApp.Web.Infrastructure.Core;
using ShoppingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShoppingApp.Web.Infrastructure.Extensions;
using System.Web.Script.Serialization;

namespace ShoppingApp.Web.Api
{
    [RoutePrefix("api/order")]
    [Authorize]
    public class OrderController: ApiControllerBase
    {
        #region innitialize
        private IProductService _productService;
        private IOrderService _orderService;

        public OrderController(IErrorService errorService, IProductService productService, IOrderService orderService) : base(errorService)
        {
            this._productService = productService;
            this._orderService = orderService;
        }
        #endregion
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
          
            return CreateHttpResponseMessage(request, () =>
            {
                int totalRow = 0;

                var model = _orderService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<ShoppingApp.Common.ViewModels.OrderFullViewModel>, IEnumerable<ShoppingApp.Common.ViewModels.OrderFullViewModel>>(query);

                var paginationSet = new PaginationList<ShoppingApp.Common.ViewModels.OrderFullViewModel>()
                {
                    Item = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = model.ToList()
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

    }
}