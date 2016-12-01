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
    public class SleepApiController : ApiController
    {
        ISimpleHealthTrackingRepository repository;
        SleepFactory sleepFactory = new SleepFactory();

        public SleepApiController()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackerContext());
        }

        public SleepApiController(ISimpleHealthTrackingRepository repo)
        {
            repository = repo;
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetLastThirtySleeps()
        {
            string userId = User.Identity.GetUserId();
            var sleeps = repository.GetLastThirtySleepsForUser(userId);

            return Json(sleeps);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetLastSleeps(int count)
        {
            string userId = User.Identity.GetUserId();
            var sleeps = repository.GetLastNumberOfSleepsForUser(userId, count);

            return Json(sleeps);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetLastFullSleeps(int count)
        {
            string userId = User.Identity.GetUserId();
            var sleeps = repository.GetLastNumberOfSleepsForUser(userId, count);

            return Json(sleeps);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult AddSleep(SleepDto sleepDto)
        {
            var userId = User.Identity.GetUserId();
            sleepDto.SetDates();
            Sleep sleep = sleepFactory.CreateSleep(sleepDto);

            sleep.TimeAdded = DateTime.Now;
            sleep.UpdateTime = DateTime.Now;
            sleep.UserId = userId;
            sleep.SetMinutesSlept();
            repository.InsertSleep(sleep);

            return Ok();
        }
    }
}