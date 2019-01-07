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
        var listPostCategoryVm = Mapper.Map < List < PostCategoryViewModel >>(listCategory);
        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVm);

        return response;
      });
    }
    [Route("add")]
    public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategiryVm)
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
          PostCategory newpostCategory = new PostCategory();
          newpostCategory.UpdatePostCategory(postCategiryVm);
          var category = _postCategoryService.Add(newpostCategory);
          _postCategoryService.Save();
          response = request.CreateResponse(HttpStatusCode.Created, category);
        }
        return response;
      });
    }
    [Route("update")]
    public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
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
          var postCategoryDb = _postCategoryService.GetById(postCategoryVm.ID);
          postCategoryDb.UpdatePostCategory(postCategoryVm);
          _postCategoryService.Update(postCategoryDb);
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