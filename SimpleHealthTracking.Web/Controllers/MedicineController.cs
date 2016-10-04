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

        [Authorize]
        public ActionResult Edit(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Medicine medicine = repository.GetMedicine(id);

            if (medicine == null)
            {
                return HttpNotFound();
            }

            if (currentUser != medicine.UserId)
            {
                return new HttpUnauthorizedResult();
            }

            return View(medicine);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Medicine medicine)
        {
            var currentUser = User.Identity.GetUserId();
            medicine.UpdateTime = DateTime.Now;

            if (currentUser != medicine.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                repository.UpdateMedicine(medicine);
                return RedirectToAction("Index");
            }

            return View(medicine);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Medicine medicine = repository.GetMedicine(id);

            if (currentUser != medicine.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (medicine == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(medicine);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Medicine medicine = repository.GetMedicine(id);

            if (currentUser != medicine.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            repository.DeleteMedicine(id);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var currentUser = User.Identity.GetUserId();
            Medicine medicine = repository.GetMedicine(id);

            if (currentUser != medicine.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (medicine == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(medicine);
        }

        [Authorize]
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            return View(repository.GetMedicinesForUser(currentUser).ToList());
        }
    }
}