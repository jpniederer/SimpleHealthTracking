namespace SimpleHealthTracking.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class CheckinViewController : Controller
    {
        // GET: CheckinView
        public ActionResult Index()
        {
            return View();
        }
    }
}