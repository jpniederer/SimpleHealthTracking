namespace SimpleHealthTracking.Web.Controllers
{
    using Excel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using SimpleHealthTracking.Repository.DTO;
    using SimpleHealthTracking.Repository.Factories;

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
            List<ExcelImportDto> excelData;

            if (file != null && file.ContentLength > 0 && file.FileName.EndsWith("xlxs"))
            {
                excelData = GetExcelData(file);
            }

            return RedirectToAction("UploadExcel");
        }

        private List<ExcelImportDto> GetExcelData(HttpPostedFileBase file)
        {
            List<ExcelImportDto> rows = new List<ExcelImportDto>();
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(file.InputStream);
            
            while (excelReader.Read())
            {

            }

            excelReader.Close();
            return rows;
        }

    }
}