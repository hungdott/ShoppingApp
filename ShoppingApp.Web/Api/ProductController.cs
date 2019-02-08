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
  [RoutePrefix("api/product")]
  [Authorize]
  public class ProductController : ApiControllerBase
  {
    #region innitialize
    private IProductService _productService;

    public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
    {
      this._productService = productService;
    }
    #endregion
    [Route("getall")]
    [HttpGet]
    public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
    {
      return CreateHttpResponseMessage(request, () =>
      {
        int totalRow = 0;

        var model = _productService.GetAll(keyword);

        totalRow = model.Count();
        var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
        var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

        var paginationSet = new PaginationSet<ProductViewModel>()
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
        var model = _productService.GetAll();

        var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);


        var response = request.CreateResponse(HttpStatusCode.OK, responseData);
        return response;
      });
    }

    [Route("create")]
    [HttpPost]
    [AllowAnonymous]
    public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productVM)
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
          var newProduct = new Product();
          newProduct.UpdateProduct(productVM);
          newProduct.CreatedDate = DateTime.Now;
          newProduct.CreatedBy = User.Identity.Name;
          _productService.Add(newProduct);
          _productService.Save();
          var responseData = Mapper.Map<Product, ProductViewModel>(newProduct);
          response = request.CreateResponse(HttpStatusCode.Created, responseData);
        }

        return response;
      });
    }
    [Route("getbyid/{id:int}")]
    [HttpGet]
    public HttpResponseMessage GetById(HttpRequestMessage request, int id)
    {
      return CreateHttpResponseMessage(request, () =>
      {
        var model = _productService.GetById(id);

        var responseData = Mapper.Map<Product, ProductViewModel>(model);


        var response = request.CreateResponse(HttpStatusCode.OK, responseData);
        return response;
      });
    }
    [Route("update")]
    [HttpPut]
    [AllowAnonymous]
    public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVM)
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
          var dbProduct = _productService.GetById(productVM.ID);
          dbProduct.UpdateProduct(productVM);
          dbProduct.UpdatedDate = DateTime.Now;
          _productService.Update(dbProduct);
          _productService.Save();
          var responseData = Mapper.Map<Product, ProductViewModel>(dbProduct);
          response = request.CreateResponse(HttpStatusCode.Created, responseData);
        }

        return response;
      });
    }
    [Route("delete")]
    [HttpDelete]
    public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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

          var oldProduct = _productService.Delete(id);
          _productService.Save();
          var responseData = Mapper.Map<Product, ProductViewModel>(oldProduct);
          response = request.CreateResponse(HttpStatusCode.OK, responseData);
        }

        return response;
      });
    }
    [Route("deletemulti")]
    [HttpDelete]
    public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProducts)
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
          var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);
          foreach (var item in listProduct)
          {
            _productService.Delete(item);
          }
          _productService.Save();

          response = request.CreateResponse(HttpStatusCode.OK, listProduct.Count);
        }

        return response;
      });
    }
  }
}
