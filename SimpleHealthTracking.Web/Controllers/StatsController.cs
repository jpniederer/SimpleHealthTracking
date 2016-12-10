namespace SimpleHealthTracking.Web.Controllers
{
    using Classes;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Repository.Entities;
    using Repository;
    using ViewModels;

    public class StatsController : Controller
    {
        ISimpleHealthTrackingRepository repository;

        public StatsController()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackerContext());
        }

        [Authorize]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            HealthStatistics hs = new HealthStatistics(userId, true);

            return View(hs);
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