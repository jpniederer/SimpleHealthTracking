namespace SimpleHealthTracking.Web.Controllers
{
    using Excel;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using SimpleHealthTracking.Repository.DTO;
    using SimpleHealthTracking.Repository.Factories;
    using SimpleHealthTracking.Web.Classes;

    public class UploadController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult UploadExcel()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadExcel(HttpPostedFileBase file)
        {
            ExcelDataConverter excelData;

            if (file != null && file.ContentLength > 0 && file.FileName.EndsWith("xlsx"))
            {
                excelData = GetExcelData(file);
                excelData.GenerateAllRecords();
            }

            return RedirectToAction("UploadExcel");
        }

        private ExcelDataConverter GetExcelData(HttpPostedFileBase file)
        {
            string userId = User.Identity.GetUserId();
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(file.InputStream);
            excelReader.IsFirstRowAsColumnNames = true;

            return new ExcelDataConverter(excelReader, "HealthForImport", userId);
        }

    }
}