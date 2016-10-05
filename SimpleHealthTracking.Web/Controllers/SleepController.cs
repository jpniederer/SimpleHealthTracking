namespace SimpleHealthTracking.Web.Controllers
{
    using SimpleHealthTracking.Repository;
    using SimpleHealthTracking.Repository.Entities;
    using SimpleHealthTracking.Repository.Factories;
    using SimpleHealthTracking.Web.ViewModels;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Net;

    public class SleepController : Controller
    {
        ISimpleHealthTrackerRepository repository;
        SleepFactory sleepFactory = new SleepFactory();

        public SleepController()
        {
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());
        }

        public SleepController(ISimpleHealthTrackerRepository repo)
        {
            repository = repo;
        }
        
        [Authorize]
        public ActionResult Create()
        {
            SleepViewModel viewModel = new SleepViewModel
            {

            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SleepViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            Sleep sleep = new Sleep
            {
                UserId = User.Identity.GetUserId(),
                StartTime = viewModel.GetStartDateTime(),
                EndTime = viewModel.GetEndDateTime(),
                SleepQuality = viewModel.GetSleepQuality(),
                TimeAdded = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            sleep.MinutesSlept = GetMinutesSlept(sleep);

            repository.InsertSleep(sleep);

            return RedirectToAction("Index", "Home");
        }

        private float GetMinutesSlept(Sleep sleep)
        {
            long startTicks = 0;
            long endTicks = 0;

            if (sleep.EndTime != null && sleep.StartTime != null)
            {
                startTicks = ((DateTime)sleep.StartTime).Ticks;
                endTicks = ((DateTime)sleep.EndTime).Ticks;
            }

            if (endTicks > startTicks)
            {
                return (float)TimeSpan.FromTicks(endTicks - startTicks).TotalMinutes;
            }

            return 0.0f;
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Sleep sleep = repository.GetSleep(id);

            if (sleep == null)
            {
                return HttpNotFound();
            }

            if (currentUser != sleep.UserId)
            {
                return new HttpUnauthorizedResult();
            }

            return View(sleep);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sleep sleep)
        {
            var currentUser = User.Identity.GetUserId();
            
            if (currentUser != sleep.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                sleep.UpdateTime = DateTime.Now;
                sleep.MinutesSlept = GetMinutesSlept(sleep);
                repository.UpdateSleep(sleep);
                return RedirectToAction("Index");
            }

            return View(sleep);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Sleep sleep = repository.GetSleep(id);

            if (currentUser != sleep.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (sleep == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(sleep);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Sleep sleep = repository.GetSleep(id);

            if (currentUser != sleep.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            repository.DeleteCheckin(id);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Sleep sleep = repository.GetSleep(id);

            if (currentUser != sleep.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (sleep == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(sleep);
        }

        [Authorize]
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            return View(repository.GetSleepForUser(currentUser).ToList());
        }
    }
}