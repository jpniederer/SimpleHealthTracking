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

    public class CheckinController : Controller
    {
        ISimpleHealthTrackerRepository repository;
        CheckinFactory checkinFactory = new CheckinFactory();

        public CheckinController()
        {
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());
        }
        
        public CheckinController(ISimpleHealthTrackerRepository repo)
        {
            repository = repo;
        }

        [Authorize]
        public ActionResult CheckinPartial()
        {
            CheckinViewModel viewModel = new CheckinViewModel();

            return View(viewModel);
        }

        // Create Checkin
        [Authorize]
        public ActionResult Create()
        {
            CheckinViewModel viewModel = new CheckinViewModel();

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CheckinViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            Checkin checkin = new Checkin
            {
                UserId = User.Identity.GetUserId(),
                Weight = viewModel.GetFloatValue(viewModel.Weight),
                Heartrate = viewModel.GetFloatValue(viewModel.Heartrate),
                SystolicBloodPressure = viewModel.GetFloatValue(viewModel.SystolicBloodPressure),
                DiastolicBloodPressure = viewModel.GetFloatValue(viewModel.DiastolicBloodPressure),
                PhysicalFeelingRating = viewModel.GetFloatValue(viewModel.PhysicalFeelingRating),
                PsychologicalFeelingRating = viewModel.GetFloatValue(viewModel.PsychologicalFeelingRating),
                ExerciseRating = viewModel.GetFloatValue(viewModel.ExerciseRating),
                TimeAdded = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            repository.InsertCheckin(checkin);

            return RedirectToAction("Index");
        }

        // Edit Checkin
        [Authorize]
        public ActionResult Edit(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Checkin checkin = repository.GetCheckin(id);

            if (checkin == null)
            {
                return HttpNotFound();
            }

            if (currentUser != checkin.UserId)
            {
                return new HttpUnauthorizedResult();
            }

            return View(checkin);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Checkin checkin)
        {
            var currentUser = User.Identity.GetUserId();

            if (currentUser != checkin.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                checkin.UpdateTime = DateTime.Now;
                repository.UpdateCheckin(checkin);
                return RedirectToAction("Index");
            }

            return View(checkin);
        }

        // Delete Checkin
        [Authorize]
        public ActionResult Delete(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Checkin checkin = repository.GetCheckin(id);

            if (currentUser != checkin.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (checkin == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(checkin);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Checkin checkin = repository.GetCheckin(id);

            if (currentUser != checkin.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            repository.DeleteCheckin(id);

            return RedirectToAction("Index");
        }

        // Checkin Details
        [Authorize]
        public ActionResult Details(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Checkin checkin = repository.GetCheckin(id);

            if (currentUser != checkin.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (checkin == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(checkin);
        }

        // Checkin Index
        [Authorize]
        public ActionResult Index(string sortOrder, int? page)
        {
            var currentUser = User.Identity.GetUserId();
            var checkinsForUser = GetCheckinsForIndex(sortOrder, currentUser);
            SetupIndexSortingViewBag(sortOrder);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(checkinsForUser.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult Notes(int? page)
        {
            var currentUser = User.Identity.GetUserId();
            var checkinsForUser = repository.GetCheckinsForUser(currentUser)
                .Where(c => !string.IsNullOrEmpty(c.Notes))
                .OrderByDescending(c => c.TimeAdded);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(checkinsForUser.ToPagedList(pageNumber, pageSize));
        }

        private void SetupIndexSortingViewBag(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParameter = String.IsNullOrEmpty(sortOrder) ? "TimeAddedAsc" : "";
            ViewBag.WeightSortParameter = sortOrder == "Weight" ? "WeightDesc" : "Weight";
            ViewBag.HeartrateSortParameter = sortOrder == "Heartrate" ? "HeartrateDesc" : "Heartrate";
        }

        private IEnumerable<Checkin> GetCheckinsForIndex(string sortOrder, string currentUser)
        {
            var checkinsForUser = repository.GetCheckinsForUser(currentUser);

            switch (sortOrder)
            {
                case "Weight":
                    return checkinsForUser.OrderBy(c => c.Weight);
                case "WeightDesc":
                    return checkinsForUser.OrderByDescending(c => c.Weight);
                case "Heartrate":
                    return checkinsForUser.OrderBy(c => c.Heartrate);
                case "HeartrateDesc":
                    return checkinsForUser.OrderByDescending(c => c.Heartrate);
                case "TimeAddedAsc":
                    return checkinsForUser.OrderBy(c => c.TimeAdded);
                default:
                    return checkinsForUser.OrderByDescending(c => c.TimeAdded);
            }
        }
    }
}