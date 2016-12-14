namespace SimpleHealthTracking.Web.Controllers
{
    using Repository;
    using Repository.Entities;
    using Repository.Factories;
    using ViewModels;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Net;
    using PagedList;

    public class MedicineController : Controller
    {
        ISimpleHealthTrackingRepository repository;
        MedicineFactory medicineFactory = new MedicineFactory();

        public MedicineController()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackingContext());
        }

        public MedicineController(ISimpleHealthTrackingRepository repo)
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
                IsPublic = viewModel.IsPublic,
                StartDate = viewModel.GetStartDate(),
                EndDate = viewModel.GetEndDate(),
                TimeAdded = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            repository.InsertMedicine(medicine);

            return RedirectToAction("Index");
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

            if (currentUser != medicine.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                medicine.UpdateTime = DateTime.Now;
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
        public ActionResult Index(string sortOrder, int? page)
        {
            var currentUser = User.Identity.GetUserId();
            var medicinesForUser = GetMedicinesForIndex(sortOrder, currentUser);
            SetupIndexSortingViewBag(sortOrder);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(medicinesForUser.ToPagedList(pageNumber, pageSize));
        }

        private void SetupIndexSortingViewBag(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewBag.NumberTimesSort = sortOrder == "NumberTimes" ? "NumberTimesDesc" : "NumberTimes";
            ViewBag.IsActiveSort = sortOrder == "IsActive" ? "IsActive" : "IsActiveDesc";
            ViewBag.StartDateSort = sortOrder == "StartDate" ? "StartDateDesc" : "StartDate";
            ViewBag.EndDateSort = sortOrder == "EndDate" ? "EndDateDesc" : "EndDate";
            ViewBag.TimeAddedSort = sortOrder == "TimeAdded" ? "TimeAddedDesc" : "TimeAdded";
        }

        private IEnumerable<Medicine> GetMedicinesForIndex(string sortOrder, string currentUser)
        {
            var medicinesForUser = repository.GetMedicinesForUser(currentUser);

            switch (sortOrder)
            {
                case "NumberTimes":
                    return medicinesForUser.OrderBy(m => m.NumberOfTimesPerDay);
                case "NumberTimesDesc":
                    return medicinesForUser.OrderByDescending(m => m.NumberOfTimesPerDay);
                case "IsActive":
                    return medicinesForUser.OrderBy(m => m.IsActive);
                case "IsActiveDesc":
                    return medicinesForUser.OrderByDescending(m => m.IsActive);
                case "StartDate":
                    return medicinesForUser.OrderBy(m => m.StartDate);
                case "StartDateDesc":
                    return medicinesForUser.OrderByDescending(m => m.StartDate);
                case "EndDate":
                    return medicinesForUser.OrderBy(m => m.EndDate);
                case "EndDateDesc":
                    return medicinesForUser.OrderByDescending(m => m.EndDate);
                case "TimeAdded":
                    return medicinesForUser.OrderBy(m => m.TimeAdded);
                case "TimeAddedDesc":
                    return medicinesForUser.OrderByDescending(m => m.TimeAdded);
                case "NameDesc":
                    return medicinesForUser.OrderByDescending(m => m.Name);
                default:
                    return medicinesForUser.OrderBy(m => m.Name);
            }
        }
    }
}