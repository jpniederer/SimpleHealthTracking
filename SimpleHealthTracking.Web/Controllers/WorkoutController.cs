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
    using PagedList;

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
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.WorkoutTypes = repository.GetWorkoutTypes()
                                   .Select(w => new { Value = w.Id, Text = w.Name });
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Workout workout)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.User = User.Identity.GetUserId();
                ViewBag.WorkoutTypes = repository.GetWorkoutTypes()
                                   .Select(w => new { Value = w.Id, Text = w.Name });

                return View("Create", workout);
            }

            workout.TimeAdded = DateTime.Now;
            workout.UpdateTime = DateTime.Now;
            repository.InsertWorkout(workout);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Workout workout = repository.GetWorkout(id);
            ViewBag.WorkoutTypes = repository.GetWorkoutTypes()
                                   .Select(w => new { Value = w.Id, Text = w.Name });

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

            ViewBag.WorkoutTypes = repository.GetWorkoutTypes()
                                   .Select(w => new { Value = w.Id, Text = w.Name });

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

        [Authorize]
        public ActionResult Details(int id)
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
        public ActionResult Index(string sortOrder, int? page)
        {
            var currentUser = User.Identity.GetUserId();
            var workoutsForUser = GetWorkoutsForIndex(sortOrder, currentUser);
            SetupIndexSortingViewBag(sortOrder);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(workoutsForUser.ToPagedList(pageNumber, pageSize));
        }

        private void SetupIndexSortingViewBag(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
        }

        private IEnumerable<Workout> GetWorkoutsForIndex(string sortOrder, string currentUser)
        {
            var workoutsForUser = repository.GetWorkoutsForUser(currentUser);

            switch (sortOrder)
            {
                case "DifficultyLevel":
                    return workoutsForUser.OrderBy(w => w.DifficultyLevel);
                default:
                    return workoutsForUser.OrderBy(w => w.DateAddedFor);
            }
        }
    }
}