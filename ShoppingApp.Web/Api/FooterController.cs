using AutoMapper;
using ShoppingApp.Model.Models;
using ShoppingApp.Service;
using ShoppingApp.Web.Infrastructure.Core;
using ShoppingApp.Web.Infrastructure.Extensions;
using ShoppingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingApp.Web.Api
{
  [RoutePrefix("api/footer")]
  [Authorize]
  public class FooterController : ApiControllerBase
    {
        private IFooterService _footerService;
        public FooterController(IErrorService errorService, IFooterService footerService):base(errorService)
        {
            this._footerService = footerService;
        }
        [Route("getfooter")]
        [HttpGet]
        public HttpResponseMessage GetContentFooter(HttpRequestMessage request,string id)
        {
            var model = _footerService.GetFooter(id);
            var responseData = Mapper.Map<Footer, FooterViewModel>(model);
            var response = request.CreateResponse(HttpStatusCode.OK, responseData);
            return response;
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, FooterViewModel footerVM)
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
                    var dbFooter = _footerService.GetFooter(footerVM.ID);
                    dbFooter.UpdateFooter(footerVM);

                    _footerService.UpdateFooter(dbFooter);
                    _footerService.Save();
                    var responseData = Mapper.Map<Footer, FooterViewModel>(dbFooter);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}
