using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenerateNewsBLL.Services;
using GenerateNewsDAL.Models;


namespace NewsAPI.Controllers
{
    public class APIController : ApiController
    {
        APIService service = new APIService();


        [HttpGet]
        [HttpPost]
        [Route(@"api/GetNewsDate")]
        public IHttpActionResult GetNewsDate(DateTime dateFrom, DateTime dateTo)
        {
            return Json(service.GetAllNewsDate(dateFrom, dateTo));
        }

        [HttpGet]
        [HttpPost]
        [Route(@"api/GetNewsToday")]
        public IHttpActionResult GetNewsToday(string theme)
        {
            return Json(service.GetNewsToday(theme));
        }
    }
}
