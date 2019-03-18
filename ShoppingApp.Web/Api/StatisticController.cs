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
    //[Authorize]
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiControllerBase
    {
        IStatisticService _statisticService;
        public StatisticController(IErrorService errorService, IStatisticService statisticService) : base(errorService)
        {
            this._statisticService = statisticService;
        }
        [Route("getrevenue")]
        [HttpGet]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage resquest,string fromDate,string toDate)
        {
            return CreateHttpResponseMessage(resquest, () =>
             {
                 var model = _statisticService.GetRevenueStatistic(fromDate, toDate);
                 HttpResponseMessage response = resquest.CreateResponse(HttpStatusCode.OK, model);
                 return response;
             });
        }
    }
}
