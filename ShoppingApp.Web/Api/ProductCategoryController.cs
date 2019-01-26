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

namespace ShoppingApp.Web.Api
{
  [RoutePrefix("api/productcategory")]
  public class ProductCategoryController : ApiControllerBase
  {
    private IProductCategoryService _productCategoryService;

    public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
    {
      this._productCategoryService = productCategoryService;
    }

    [Route("getall")]
    [HttpGet]
    public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
    {
      return CreateHttpResponseMessage(request, () =>
      {
        int totalRow = 0;

        var model = _productCategoryService.GetAll(keyword);

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
    [Route("getallparents")]
    [HttpGet]
    public HttpResponseMessage GetAll(HttpRequestMessage request)
    {
      return CreateHttpResponseMessage(request, () =>
      {
        var model = _productCategoryService.GetAll();
              
        var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);

       
        var response = request.CreateResponse(HttpStatusCode.OK, responseData);
        return response;
      });
    }

    [Route("create")]
    [HttpPost]
    [AllowAnonymous]
    public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
    {
      return CreateHttpResponseMessage(request, () =>
      {
        HttpResponseMessage response = null;
        if (ModelState.IsValid == false)
        {
          response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }
        else
        {
          var newProductCategory = new ProductCategory();
          newProductCategory.UpdateProductCategory(productCategoryVM);
          _productCategoryService.Add(newProductCategory);
          _productCategoryService.Save();
          var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
          response = request.CreateResponse(HttpStatusCode.Created, responseData);
        }
        
        return response;
      });
    }
  }
}