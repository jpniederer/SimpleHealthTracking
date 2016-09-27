namespace SimpleHealthTracking.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class CheckinController : Controller
    {
        // GET: CheckinView
        public ActionResult Create()
        {
            return View();
        }
    }
}