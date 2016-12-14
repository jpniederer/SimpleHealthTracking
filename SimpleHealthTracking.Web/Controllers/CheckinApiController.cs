namespace SimpleHealthTracking.Web.Controllers
{
    using Repository.Entities;
    using Repository.DTO;
    using Repository.Factories;
    using Repository;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Web.Http;

    [Authorize]
    public class CheckinApiController : ApiController
    {
        ISimpleHealthTrackingRepository repository;
        CheckinFactory checkinFactory = new CheckinFactory();

        public CheckinApiController()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackingContext());
        }

        public CheckinApiController(ISimpleHealthTrackingRepository repo)
        {
            repository = repo;
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetLastCheckins(int count)
        {
            string userId = User.Identity.GetUserId();
            var checkins = repository.GetLastNumberOfCheckinsForUser(userId, count);

            return Json(checkins);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetLastCheckinsForWeights(int count)
        {
            string userId = User.Identity.GetUserId();
            var checkins = repository.GetLastNumberOfCheckinWeightsForUser(userId, count);

            return Json(checkins);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetLastCheckinsForHeartrates(int count)
        {
            string userId = User.Identity.GetUserId();
            var checkins = repository.GetLastNumberOfCheckinHeartratesForUser(userId, count);

            return Json(checkins);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult AddCheckin(CheckinDto checkinDto)
        {
            var userId = User.Identity.GetUserId();
            Checkin checkin = checkinFactory.CreateCheckin(checkinDto);
            DateTime time;

            if (DateTime.TryParse(checkinDto.TimeString, out time))
            {
                checkin.TimeAdded = time;
            }
            else
            {
                checkin.TimeAdded = DateTime.Now;
            }

            checkin.UpdateTime = DateTime.Now;
            checkin.UserId = userId;
            repository.InsertCheckin(checkin);

            return Ok();
        }
    }
}