namespace SimpleHealthTracking.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class MedicineViewController : Controller
    {
        // GET: MedicineView
        public ActionResult Index()
        {
            return View();
        }
    }
}