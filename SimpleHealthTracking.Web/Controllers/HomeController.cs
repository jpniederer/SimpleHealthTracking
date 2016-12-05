namespace SimpleHealthTracking.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Web.Mvc;
    using Repository;
    using Repository.Entities;

    public class HomeController : Controller
    {
        ISimpleHealthTrackingRepository repository;

        public HomeController()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackerContext());
        }

        [Authorize]
        public ActionResult Index()
        {
            string currentUser = User.Identity.GetUserId();
            ViewBag.MedicinesForUser = repository.GetMedicinesForUser(currentUser)
                                       .Select(m => new { Value = m.Id, Text = m.Name });
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }
    }
}