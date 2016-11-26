namespace SimpleHealthTracking.Web.Controllers
{
    using SimpleHealthTracking.Repository.Entities;
    using SimpleHealthTracking.Repository.DTO;
    using SimpleHealthTracking.Repository.Factories;
    using SimpleHealthTracking.Repository;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize]
    public class SleepApiController : ApiController
    {
        ISimpleHealthTrackerRepository repository;
        SleepFactory sleepFactory = new SleepFactory();

        public SleepApiController()
        {
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());
        }

        public SleepApiController(ISimpleHealthTrackerRepository repo)
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