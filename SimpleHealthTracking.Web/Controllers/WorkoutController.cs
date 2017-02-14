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

        [Authorize]
        public ActionResult Edit(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Workout workout = repository.GetWorkout(id);

            if (workout == null)
            {
                return HttpNotFound();
            }

            if (currentUser != workout.UserId)
            {
                return new HttpUnauthorizedResult();
            }

            return View(workout);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Workout workout)
        {
            var currentUser = User.Identity.GetUserId();

            if (currentUser != workout.UserId)
            {
                return new HttpUnauthorizedResult();
            }

            if (ModelState.IsValid)
            {
                workout.UpdateTime = DateTime.Now;
                repository.UpdateWorkout(workout);
                return RedirectToAction("Index");
            }

            return View(workout);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Workout workout = repository.GetWorkout(id);

            if (currentUser != workout.UserId || workout == null)
            {
                return new HttpUnauthorizedResult();
            }

            return View(workout);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Workout workout = repository.GetWorkout(id);

            if (currentUser != workout.UserId || workout == null)
            {
                return new HttpUnauthorizedResult();
            }

            return View(workout);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}