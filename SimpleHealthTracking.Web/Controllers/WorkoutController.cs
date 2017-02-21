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
    using ViewModels;

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
            WorkoutViewModel viewModel = new WorkoutViewModel();
            ViewBag.WorkoutTypes = repository.GetWorkoutTypes()
                                   .Select(w => new { Value = w.Id, Text = w.Name });
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkoutViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.User = User.Identity.GetUserId();
                ViewBag.WorkoutTypes = repository.GetWorkoutTypes()
                                   .Select(w => new { Value = w.Id, Text = w.Name });

                return View("Create", viewModel);
            }

            Workout workout = new Workout
            {
                UserId = User.Identity.GetUserId(),
                TimeAdded = DateTime.Now,
                UpdateTime = DateTime.Now,
                WorkoutTypeId = viewModel.WorkoutTypeId,
                DateAddedFor = viewModel.GetDateAddedFor(),
                Notes = viewModel.Notes,
                KeyTakeaways = viewModel.KeyTakeaways,
                PreFeeling = viewModel.PreFeeling,
                PostFeeling = viewModel.PostFeeling,
                DifficultyLevel = viewModel.DifficultyLevel,
                LengthInMinutes = viewModel.LengthInMinutes
            };
            
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
            workout.WorkoutType = repository.GetWorkoutType(workout.WorkoutTypeId);

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

            repository.DeleteWorkout(id);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Workout workout = repository.GetWorkout(id);
            workout.WorkoutType = repository.GetWorkoutType(workout.WorkoutTypeId);

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
            List<WorkoutType> workoutTypes = repository.GetWorkoutTypes().ToList();
            var workoutsForUser = GetWorkoutsForIndex(sortOrder, currentUser);
            SetupIndexSortingViewBag(sortOrder);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(workoutsForUser.ToPagedList(pageNumber, pageSize));
        }

        private void SetupIndexSortingViewBag(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.WorkoutDateSort = string.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.DifficultySort = sortOrder == "Difficulty" ? "DifficultyDesc" : "Difficulty";
            ViewBag.PostFeelingSort = sortOrder == "PostFeeling" ? "PostFeelingDesc" : "PostFeeling";
            ViewBag.PreFeelingSort = sortOrder == "PreFeeling" ? "PreFeelingDesc" : "PreFeeling";
            ViewBag.LengthSort = sortOrder == "Length" ? "LengthDesc" : "Length";
            ViewBag.TypeSort = sortOrder == "Type" ? "TypeDesc" : "Type";
        }

        private IEnumerable<Workout> GetWorkoutsForIndex(string sortOrder, string currentUser)
        {
            var workoutsForUser = repository.GetWorkoutsForUser(currentUser);

            switch (sortOrder)
            {
                case "Difficulty":
                    return workoutsForUser.OrderBy(w => w.DifficultyLevel);
                case "DifficultyDesc":
                    return workoutsForUser.OrderByDescending(w => w.DifficultyLevel);
                case "PostFeeling":
                    return workoutsForUser.OrderBy(w => w.PostFeeling);
                case "PostFeelingDesc":
                    return workoutsForUser.OrderByDescending(w => w.PostFeeling);
                case "PreFeeling":
                    return workoutsForUser.OrderBy(w => w.PreFeeling);
                case "PreFeelingDesc":
                    return workoutsForUser.OrderByDescending(w => w.PreFeeling);
                case "Length":
                    return workoutsForUser.OrderBy(w => w.LengthInMinutes);
                case "LengthDesc":
                    return workoutsForUser.OrderByDescending(w => w.LengthInMinutes);
                case "Type":
                    return workoutsForUser.OrderBy(w => w.WorkoutType.Name);
                case "TypeDesc":
                    return workoutsForUser.OrderByDescending(w => w.WorkoutType.Name);
                case "Date":
                    return workoutsForUser.OrderBy(w => w.DateAddedFor);
                default:
                    return workoutsForUser.OrderByDescending(w => w.DateAddedFor);
            }
        }
    }
}