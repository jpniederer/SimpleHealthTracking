namespace SimpleHealthTracking.Repository.Factories
{
    using System.Linq;
    using System.Collections.Generic;
    using DTO;
    using Entities;

    public class MedicineFactory
    {
        MedicineTakenFactory mtf = new MedicineTakenFactory();
        public MedicineFactory() { }

        public MedicineDto CreateMedicine(Medicine medicine)
        {
            return new MedicineDto()
            {
                Id = medicine.Id,
                UserId = medicine.UserId,
                Name = medicine.Name,
                NumberOfTimesPerDay = medicine.NumberOfTimesPerDay,
                IsActive = medicine.IsActive,
                IsPublic = medicine.IsPublic,
                StartDate = medicine.StartDate,
                EndDate = medicine.EndDate,
                TimeAdded = medicine.TimeAdded,
                UpdateTime = medicine.UpdateTime,
                MedicineTakens = medicine.MedicineTakens == null ? new List<MedicineTakenFullDto>() : medicine.MedicineTakens.Select(m => mtf.CreateMedicineTaken(m)).ToList()
            };
        }

        public Medicine CreateMedicine(MedicineDto medicine)
        {
            return new Medicine()
            {
                Id = medicine.Id,
                UserId = medicine.UserId,
                Name = medicine.Name,
                NumberOfTimesPerDay = medicine.NumberOfTimesPerDay,
                IsActive = medicine.IsActive,
                IsPublic = medicine.IsPublic,
                StartDate = medicine.StartDate,
                EndDate = medicine.EndDate,
                TimeAdded = medicine.TimeAdded,
                UpdateTime = medicine.UpdateTime,
                MedicineTakens = medicine.MedicineTakens == null ? new List<MedicineTaken>() : medicine.MedicineTakens.Select(m => mtf.CreateMedicineTaken(m)).ToList()
            };
        }
    }
}