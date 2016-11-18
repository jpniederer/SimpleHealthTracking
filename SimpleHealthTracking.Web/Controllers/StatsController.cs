﻿namespace SimpleHealthTracking.Web.Controllers
{
    using Classes;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class StatsController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            HealthStatistics hs = new HealthStatistics(userId, true);

            return View(hs);
        }
    }
}