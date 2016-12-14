namespace SimpleHealthTracking.Web.Classes
{
    using Excel;
    using Repository.DTO;
    using Repository.Entities;
    using Repository.Factories;
    using Repository;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class ExcelDataConverter
    {
        public List<ExcelImportDto> ExcelImportRecords { get; private set; }
        public List<Checkin> Checkins { get; private set; }
        public List<MedicineTaken> MedicinesTaken { get; private set; }
        public List<Sleep> Sleeps { get; private set; }
        public string WorksheetName { get; set; }
        public string UserId { get; set; }

        private DataSet excelDataSet;
        private DataTable excelWorksheet;
        private CheckinFactory checkinFactory = new CheckinFactory();
        private MedicineTakenFactory medicineTakenFactory = new MedicineTakenFactory();
        private SleepFactory sleepFactory = new SleepFactory();
        ISimpleHealthTrackingRepository repository;

        public ExcelDataConverter()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackingContext());
        }

        public ExcelDataConverter(IExcelDataReader rawExcel, string worksheetName, string userId)
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackingContext());
            UserId = userId;
            WorksheetName = worksheetName;
            excelDataSet = rawExcel.AsDataSet();
            excelWorksheet = excelDataSet.Tables[WorksheetName];
            SetupExcelImports();
            SetupCheckins();
            SetupMedicinesTaken();
            SetupSleeps();
        }

        private void SetupExcelImports()
        {
            IEnumerable<DataRow> rows = from DataRow row in excelWorksheet.Rows
                                        where row["Date"].ToString() != ""
                                        select row;
            int medicineId = GetFirstActiveMedicineId();

            ExcelImportRecords = rows.Select(row => new ExcelImportDto()
            {
                State = row["State"].ToString(),
                DateEntry = row["Date"].ToString(),
                TimeEntry = row["Time"].ToString(),
                Weight = row["Weight (lbs)"].ToString(),
                Heartrate = row["Heartrate (bpm)"].ToString(),
                Notes = row["Notes"].ToString(),
                Medicine = row["Took Pills"].ToString(),
                Mind = row["Mind"].ToString(),
                Body = row["Body"].ToString(),
                Feel = row["Feel"].ToString(),
                UserId = UserId,
                MedicineId = medicineId
            }).ToList();
        }

        private int GetFirstActiveMedicineId()
        {
            List<Medicine> medicines = repository.GetMedicinesForUser(UserId).ToList();

            foreach (var medicine in medicines)
            {
                if (medicine.IsActive)
                    return medicine.Id;
            }

            return 0;
        }

        private void SetupCheckins()
        {
            Checkins = new List<Checkin>();

            foreach (var excelRecord in ExcelImportRecords)
            {
                Checkins.Add(checkinFactory.CreateCheckin(excelRecord));
            }
        }

        private void SetupMedicinesTaken()
        {
            MedicinesTaken = new List<MedicineTaken>();

            foreach (var excelRecord in ExcelImportRecords)
            {
                if (excelRecord.Medicine != null && excelRecord.Medicine != "" && excelRecord.MedicineId != 0)
                {
                    MedicinesTaken.Add(medicineTakenFactory.CreateMedicineTaken(excelRecord));
                }
            }
        }

        private void SetupSleeps()
        {
            Sleeps = new List<Sleep>();

            for (int i = 0; i < ExcelImportRecords.Count; i = i + 2)
            {
                if (ExcelImportRecords[i].State == "Bed" && ExcelImportRecords[i + 1].State == "Awake")
                {
                    Sleeps.Add(sleepFactory.CreateSleep(ExcelImportRecords[i], ExcelImportRecords[i + 1]));
                }
            }
        }

        public void GenerateAllRecords()
        {
            GenerateCheckins();
            GenerateMedicinesTaken();
            GenerateSleeps();
        }

        private void GenerateCheckins()
        {
            foreach(var checkin in Checkins)
            {
                repository.InsertCheckin(checkin);
            }
        }

        private void GenerateMedicinesTaken()
        {
            foreach (var med in MedicinesTaken)
            {
                repository.InsertMedicineTaken(med);
            }
        }

        private void GenerateSleeps()
        {
            foreach (var sleep in Sleeps)
            {
                repository.InsertSleep(sleep);
            }
        }
    }
}