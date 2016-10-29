namespace SimpleHealthTracking.Repository.Factories
{
    using DTO;
    using Entities;
    using Helpers;
    using System;

    public class MedicineTakenFactory
    {
        public MedicineTakenFactory() { }

        public MedicineTakenFullDto CreateMedicineTaken(MedicineTaken medicineTaken)
        {
            return new MedicineTakenFullDto()
            {
                Id = medicineTaken.Id,
                MedicineId = medicineTaken.MedicineId,
                DateAddedFor = medicineTaken.DateAddedFor,
                TimeAdded = medicineTaken.TimeAdded
            };
        }

        public MedicineTaken CreateMedicineTaken(MedicineTakenFullDto medicineTaken)
        {
            return new MedicineTaken()
            {
                Id = medicineTaken.Id,
                MedicineId = medicineTaken.MedicineId,
                DateAddedFor = medicineTaken.DateAddedFor,
                TimeAdded = medicineTaken.TimeAdded
            };
        }

        // Verify that the Medicine field of the excel record is not null. If
        // the medicine field isn't null then the medicine was taken.
        public MedicineTaken CreateMedicineTaken(ExcelImportDto excelImportDto)
        {
            return new MedicineTaken()
            {
                MedicineId = excelImportDto.MedicineId,
                DateAddedFor = DateTime.Parse(string.Format("{0} {1}", 
                    Utility.GetDateString(excelImportDto.DateEntry), 
                    Utility.GetTimeString(excelImportDto.TimeEntry))),
                TimeAdded = DateTime.Now
            };
        }
    }
}