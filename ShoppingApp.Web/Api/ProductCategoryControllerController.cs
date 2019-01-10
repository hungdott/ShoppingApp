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

namespace ShoppingApp.Web.Api
{
  [RoutePrefix("api/productcategory")]
  public class ProductCategoryControllerController : ApiControllerBase
  {
    private IProductCategoryService _productCategoryService;

    public ProductCategoryControllerController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
    {
      this._productCategoryService = productCategoryService;
    }

    [Route("getall")]
    public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
    {
      return CreateHttpResponseMessage(request, () =>
      {
        int totalRow = 0;

        var model = _productCategoryService.GetAll();

        totalRow = model.Count();
        var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
        var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

        var paginationSet = new PaginationSet<ProductCategoryViewModel>()
        {
          Item = responseData,
          Page = page,
          TotalCount = totalRow,
          TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
        };
        var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
        return response;
      });
    }
  }
}