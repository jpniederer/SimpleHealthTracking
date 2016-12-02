namespace SimpleHealthTracking.Controllers
{
    using Web.Classes;
    using Microsoft.AspNet.Identity;
    using Repository;
    using Repository.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using PagedList;
    using Web.ViewModels;

    public class MedicineTakenController : Controller
    {
        ISimpleHealthTrackingRepository repository;

        public MedicineTakenController()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackerContext());
        }

        [Authorize]
        public ActionResult MedicineTakenPartial()
        {
            string currentUser = User.Identity.GetUserId();
            ViewBag.MedicinesForUser = repository.GetMedicinesForUser(currentUser)
                                       .Select(m => new { Value = m.Id, Text = m.Name });
            MedicineTakenViewModel viewModel = new MedicineTakenViewModel();
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Streaks()
        {
            string userId = User.Identity.GetUserId();
            List<Medicine> medicines = repository.GetMedicinesForUser(userId).ToList();
            List<MedicineStats> medicineStats = new List<MedicineStats>();

            foreach (var medicine in medicines)
            {
                medicineStats.Add(new MedicineStats(medicine));
            }

            return View(medicineStats);
        }

        [Authorize]
        public ActionResult Create()
        {
            MedicineTakenViewModel viewModel = new MedicineTakenViewModel();
            string currentUser = User.Identity.GetUserId();
            ViewBag.MedicinesForUser = repository.GetMedicinesForUser(currentUser)
                                       .Select(m => new { Value = m.Id, Text = m.Name });

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicineTakenViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                string currentUser = User.Identity.GetUserId();
                ViewBag.MedicinesForUser = repository.GetMedicinesForUser(currentUser)
                                       .Select(m => new { Value = m.Id, Text = m.Name });
                return View("Create", viewModel);
            }

            MedicineTaken medicineTaken = new MedicineTaken
            {
                MedicineId = viewModel.MedicineId,
                DateAddedFor = viewModel.GetDateTimeAddedFor(),
                TimeAdded = DateTime.Now
            };

            repository.InsertMedicineTaken(medicineTaken);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            string currentUser = User.Identity.GetUserId();
            MedicineTaken medicineTaken = repository.GetMedicineTaken(id);

            if (medicineTaken == null)
            {
                return HttpNotFound();
            }

            ViewBag.MedicinesForUser = repository.GetMedicinesForUser(currentUser)
                                       .Select(m => new { Value = m.Id, Text = m.Name });
            return View(medicineTaken);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicineTaken medicineTaken)
        {
            string currentUser = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                medicineTaken.TimeAdded = DateTime.Now;
                repository.UpdateMedicineTaken(medicineTaken);
                return RedirectToAction("Index");
            }

            ViewBag.MedicinesForUser = repository.GetMedicinesForUser(currentUser)
                                       .Select(m => new { Value = m.Id, Text = m.Name });
            return View(medicineTaken);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            MedicineTaken medicineTaken = repository.GetMedicineTaken(id);
            medicineTaken.Medicine = repository.GetMedicine(medicineTaken.MedicineId);

            if (medicineTaken == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(medicineTaken);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMedicineTaken(int id)
        {
            MedicineTaken medicineTaken = repository.GetMedicineTaken(id);

            repository.DeleteMedicineTaken(id);
            return RedirectToAction("Index");
        }

        // GET: MedicineTakenView
        [Authorize]
        public ActionResult Index(string sortOrder, int? page)
        {
            string currentUser = User.Identity.GetUserId();
            List<Medicine> medicinesForUser = repository.GetMedicinesForUser(currentUser).ToList();
            var medicinesTakenForUser = GetMedicinesTakenForIndex(sortOrder, currentUser, medicinesForUser);
            SetupIndexSortingViewBag(sortOrder);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(medicinesTakenForUser.ToPagedList(pageNumber, pageSize));
        }

        private void SetupIndexSortingViewBag(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParameter = string.IsNullOrEmpty(sortOrder) ? "DateAsc" : "";
            ViewBag.MedicineNameParameter = sortOrder == "Medicine" ? "MedicineDesc" : "Medicine";
        }

        private IEnumerable<MedicineTaken> GetMedicinesTakenForIndex(string sortOrder, string currentUser, List<Medicine> medicines)
        {
            var mts = repository.GetMedicineTakenByUser(currentUser);
            List<MedicineTaken> medicinesTaken = new List<MedicineTaken>();

            foreach (var mt in mts)
            {
                mt.Medicine = medicines.SingleOrDefault(m => m.Id == mt.MedicineId);
                medicinesTaken.Add(mt);
            }

            switch (sortOrder)
            {
                case "Medicine":
                    return medicinesTaken.OrderBy(m => m.Medicine.Name);
                case "MedicineDesc":
                    return medicinesTaken.OrderByDescending(m => m.Medicine.Name);
                case "DateAsc":
                    return medicinesTaken.OrderBy(m => m.DateAddedFor);
                default:
                    return medicinesTaken.OrderByDescending(m => m.DateAddedFor);
            }
        }
    }
}