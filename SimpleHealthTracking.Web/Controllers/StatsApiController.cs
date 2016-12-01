namespace SimpleHealthTracking.Web.Controllers
{
    using Classes;
    using Repository.Entities;
    using Microsoft.AspNet.Identity;
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

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAverageWeight()
        {
            string userId = User.Identity.GetUserId();
            HealthStatistics hs = new HealthStatistics(userId);
            hs.GetAverageWeight();

            return Json(hs.AverageWeight);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAverageHeartrate()
        {
            string userId = User.Identity.GetUserId();
            HealthStatistics hs = new HealthStatistics(userId);
            hs.GetAverageHeartrate();

            return Json(hs.AverageHeartrate);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAverageMinutesSlept()
        {
            string userId = User.Identity.GetUserId();
            HealthStatistics hs = new HealthStatistics(userId);
            hs.GetAverageMinutesSlept();

            return Json(hs.AverageMinutesSlept);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAverageSleepStartTime()
        {
            string userId = User.Identity.GetUserId();
            HealthStatistics hs = new HealthStatistics(userId);
            hs.SetAverageStartAndEndSleepTimes();

            return Json(hs.AverageSleepStartTime);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAverageSleepEndTime()
        {
            string userId = User.Identity.GetUserId();
            HealthStatistics hs = new HealthStatistics(userId);
            hs.SetAverageStartAndEndSleepTimes();

            return Json(hs.AverageSleepEndTime);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetHealthStats()
        {
            string userId = User.Identity.GetUserId();
            HealthStatistics hs = new HealthStatistics(userId, true);

            return Json(hs);
        }
    }
}