namespace SimpleHealthTracking.Repository.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SimpleHealthTracking.Repository.DTO;
    using SimpleHealthTracking.Repository.Entities;

    public class ExcelImportListGenerator
    {
        public List<Sleep> SleepRecords { get; private set; }
        public List<Checkin> CheckinRecords { get; private set; }
        public List<MedicineTaken> MedicineTaken { get; private set; }
        public ExcelImportListGenerator() { }

        public ExcelImportListGenerator(List<ExcelImportDto> excelRecords)
        {

        }
    }
}