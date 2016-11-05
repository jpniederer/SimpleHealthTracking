namespace SimpleHealthTracking.Controllers
{
    using Microsoft.AspNet.Identity;
    using Repository;
    using Repository.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using PagedList;

    public class MedicineTakenController : Controller
    {
        ISimpleHealthTrackerRepository repository;

        public MedicineTakenController()
        {
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());
        }

        // GET: MedicineTakenView
        [Authorize]
        public ActionResult Index(string sortOrder, int? page)
        {
            var currentUser = User.Identity.GetUserId();
            var medicinesTakenForUser = GetMedicinesTakenForIndex(sortOrder, currentUser);
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

        private IEnumerable<MedicineTaken> GetMedicinesTakenForIndex(string sortOrder, string currentUser)
        {
            var mts = repository.GetMedicineTakenByUser(currentUser);

            switch (sortOrder)
            {
                case "Medicine":
                    return mts.OrderBy(m => m.Medicine.Name);
                case "MedicineDesc":
                    return mts.OrderByDescending(m => m.Medicine.Name);
                case "DateAsc":
                    return mts.OrderBy(m => m.TimeAdded);
                default:
                    return mts.OrderByDescending(m => m.TimeAdded);
            }
        }
    }
}