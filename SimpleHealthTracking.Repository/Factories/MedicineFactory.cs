namespace SimpleHealthTracking.Repository.Factories
{
    using System.Linq;
    using System.Collections.Generic;

    public class MedicineFactory
    {
        MedicineTakenFactory mtf = new MedicineTakenFactory();
        public MedicineFactory() { }

        public DTO.MedicineDto CreateMedicine(Entities.Medicine medicine)
        {
            return new DTO.MedicineDto()
            {
                Id = medicine.Id,
                UserId = medicine.UserId,
                Name = medicine.Name,
                NumberOfTimesPerDay = medicine.NumberOfTimesPerDay,
                IsActive = medicine.IsActive,
                StartDate = medicine.StartDate,
                EndDate = medicine.EndDate,
                TimeAdded = medicine.TimeAdded,
                UpdateTime = medicine.UpdateTime,
                MedicineTakens = medicine.MedicineTakens == null ? new List<DTO.MedicineTaken>() : medicine.MedicineTakens.Select(m => mtf.CreateMedicineTaken(m)).ToList()
            };
        }

        public Entities.Medicine CreateMedicine(DTO.MedicineDto medicine)
        {
            return new Entities.Medicine()
            {
                Id = medicine.Id,
                UserId = medicine.UserId,
                Name = medicine.Name,
                NumberOfTimesPerDay = medicine.NumberOfTimesPerDay,
                IsActive = medicine.IsActive,
                StartDate = medicine.StartDate,
                EndDate = medicine.EndDate,
                TimeAdded = medicine.TimeAdded,
                UpdateTime = medicine.UpdateTime,
                MedicineTakens = medicine.MedicineTakens == null ? new List<Entities.MedicineTaken>() : medicine.MedicineTakens.Select(m => mtf.CreateMedicineTaken(m)).ToList()
            };
        }
    }
}