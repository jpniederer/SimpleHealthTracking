namespace SimpleHealthTracking.Web.Controllers
{
    using Classes;
    using Repository.Entities;
    using Repository;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class StatsApiController : ApiController
    {
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetMaxWeightCheckin()
        {
            string userId = User.Identity.GetUserId();
            HealthStatistics hs = new HealthStatistics(userId);
            Checkin maxWeightCheckin = hs.GetMaxWeight();

            return Json(maxWeightCheckin);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetMaxHeartrateCheckin()
        {
            string userId = User.Identity.GetUserId();
            HealthStatistics hs = new HealthStatistics(userId);
            Checkin maxHeartrateCheckin = hs.GetMaxHeartrate();

            return Json(maxHeartrateCheckin);
        }
    }
}