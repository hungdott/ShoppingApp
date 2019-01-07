using ShoppingApp.Model.Models;
using ShoppingApp.Service;
using ShoppingApp.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingApp.Web.Api
{
  [RoutePrefix("api/postcategory")]
  public class PostCategoryController : ApiControllerBase
  {
    IPostCategoryService _postCategoryService;

    public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
    {
      this._postCategoryService = postCategoryService;
    }
    [Route("getall")]
    public HttpResponseMessage Get(HttpRequestMessage request)
    {
      return CreateHttpResponseMessage(request, () =>
      {

        var listCategory = _postCategoryService.GetAll();

        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCategory);

        return response;
      });
    }
    public HttpResponseMessage Post(HttpRequestMessage request, PostCategory postCategiry)
    {
      return CreateHttpResponseMessage(request, () =>
      {
        HttpResponseMessage response = null;
        if (ModelState.IsValid)
        {
          response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
        else
        {
          var category = _postCategoryService.Add(postCategiry);
          _postCategoryService.Save();
          response = request.CreateResponse(HttpStatusCode.Created, category);
        }
        return response;
      });
    }
    public HttpResponseMessage Put(HttpRequestMessage request, PostCategory postCategiry)
    {
      return CreateHttpResponseMessage(request, () =>
      {
        HttpResponseMessage response = null;
        if (ModelState.IsValid)
        {
          response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
        else
        {
          _postCategoryService.Update(postCategiry);
          _postCategoryService.Save();
          response = request.CreateResponse(HttpStatusCode.OK);
        }
        return response;
      });
    }
    public HttpResponseMessage Delete(HttpRequestMessage request, int id)
    {
      return CreateHttpResponseMessage(request, () =>
      {
        HttpResponseMessage response = null;
        if (ModelState.IsValid)
        {
          response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
        else
        {
          _postCategoryService.Delete(id);
          _postCategoryService.Save();
          response = request.CreateResponse(HttpStatusCode.OK);
        }
        return response;
      });
    }

  }
}