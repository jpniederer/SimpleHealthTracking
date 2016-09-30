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

    public class MedicineController : Controller
    {
        ISimpleHealthTrackerRepository repository;
        MedicineFactory medicineFactory = new MedicineFactory();

        public MedicineController()
        {
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());
        }

        public MedicineController(ISimpleHealthTrackerRepository repo)
        {
            repository = repo;
        }

        [Authorize]
        public ActionResult Create()
        {
            MedicineViewModel viewModel = new MedicineViewModel();

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicineViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            Medicine medicine = new Medicine
            {
                UserId = User.Identity.GetUserId(),
                Name = viewModel.Name,
                NumberOfTimesPerDay = viewModel.NumberOfTimesPerDay,
                IsActive = viewModel.IsActive,
                StartDate = viewModel.GetStartDate(),
                EndDate = viewModel.GetEndDate(),
                TimeAdded = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            repository.InsertMedicine(medicine);

            return RedirectToAction("Index", "Home");
        }
    }
}