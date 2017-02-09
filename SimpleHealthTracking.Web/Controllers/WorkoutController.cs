namespace SimpleHealthTracking.Web.Controllers
{
    using Repository;
    using Repository.Entities;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class WorkoutController : Controller
    {
        ISimpleHealthTrackingRepository repository;

        public WorkoutController()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackingContext());
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", workout);
            }

            repository.InsertWorkout(workout);
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}