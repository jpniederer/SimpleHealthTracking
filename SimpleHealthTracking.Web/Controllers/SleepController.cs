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
    using PagedList;

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

            return RedirectToAction("Index");
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

            repository.DeleteSleep(id);

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
        public ActionResult Index(string sortOrder, int? page)
        {
            var currentUser = User.Identity.GetUserId();
            var sleepsForUser = GetSleepsForIndex(sortOrder, currentUser);
            SetupIndexSortingViewBag(sortOrder);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(sleepsForUser.ToPagedList(pageNumber, pageSize));
        }

        private void SetupIndexSortingViewBag(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParameter = String.IsNullOrEmpty(sortOrder) ? "TimeAddedAsc" : "";
            ViewBag.StartTimeParameter = sortOrder == "StartTime" ? "StartTimeDesc" : "StartTime";
            ViewBag.EndTimeParameter = sortOrder == "EndTime" ? "EndTimeDesc" : "EndTime";
            ViewBag.MinutesSleptParameter = sortOrder == "MinutesSlept" ? "MinutesSleptDesc" : "MinutesSlept";
            ViewBag.SleepQualityParameter = sortOrder == "SleepQuality" ? "SleepQualityDesc" : "SleepQuality";
        }

        private IEnumerable<Sleep> GetSleepsForIndex(string sortOrder, string currentUser)
        {
            var sleepsForUser = repository.GetSleepForUser(currentUser);

            switch (sortOrder)
            {
                case "StartTime":
                    return sleepsForUser.OrderBy(s => s.StartTime);
                case "StartTimeDesc":
                    return sleepsForUser.OrderByDescending(s => s.StartTime);
                case "EndTime":
                    return sleepsForUser.OrderBy(s => s.EndTime);
                case "EndTimeDesc":
                    return sleepsForUser.OrderByDescending(s => s.EndTime);
                case "MinutesSlept":
                    return sleepsForUser.OrderBy(s => s.MinutesSlept);
                case "MinutesSleptDesc":
                    return sleepsForUser.OrderByDescending(s => s.MinutesSlept);
                case "SleepQuality":
                    return sleepsForUser.OrderBy(s => s.SleepQuality);
                case "SleepQualityDesc":
                    return sleepsForUser.OrderByDescending(s => s.SleepQuality);
                case "TimeAddedAsc":
                    return sleepsForUser.OrderBy(s => s.TimeAdded);
                default:
                    return sleepsForUser.OrderByDescending(s => s.TimeAdded);
            }
        }
    }
}