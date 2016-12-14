namespace SimpleHealthTracking.Web.Controllers
{
    using Classes;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Repository.Entities;
    using Repository;
    using ViewModels;

    public class StatsController : Controller
    {
        ISimpleHealthTrackingRepository repository;

        public StatsController()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackingContext());
        }

        [Authorize]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            HealthStatistics hs = new HealthStatistics(userId, true);

            return View(hs);
        }

        [Authorize]
        public ActionResult PublicStatsMaintenance()
        {
            string userId = User.Identity.GetUserId();
            PublicStatsPage psp = repository.GetPublicStatsPageForUser(userId);

            if (psp == null)
            {
                psp = new PublicStatsPage
                {
                    UserId = userId,
                    IsVisible = true,
                    TimeAdded = DateTime.Now,
                    UpdateTime = DateTime.Now
                };
                
                repository.InsertPublicStatsPage(psp);
            }

            return View(psp);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PublicStatsMaintenance(PublicStatsPage psp)
        {
            string userId = User.Identity.GetUserId();

            if (userId != psp.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                psp.UpdateTime = DateTime.Now;
                repository.UpdatePublicStatsPage(psp);
                return RedirectToAction("PublicStats", new { id = psp.Id });
            }

            return View(psp);
        }

        public ActionResult PublicStats(int id)
        {
            PublicStatsPage psp = repository.GetPublicStatsPage(id);
            List<Medicine> medicines = repository.GetPublicMedicineForUser(psp.UserId).ToList();
            PublicStatsViewModel publicStatsViewModel = new PublicStatsViewModel(psp, medicines);

            return View(publicStatsViewModel);
        }
    }
}