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
    }
}