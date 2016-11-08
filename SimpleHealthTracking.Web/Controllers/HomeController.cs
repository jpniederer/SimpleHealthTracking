namespace SimpleHealthTracking.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Repository;
    using Repository.Entities;

    public class HomeController : Controller
    {
        ISimpleHealthTrackerRepository repository;

        public HomeController()
        {
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());
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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}