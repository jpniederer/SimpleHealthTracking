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

            return RedirectToAction("Index", "Home");
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
    }
}