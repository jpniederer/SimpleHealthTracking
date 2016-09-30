namespace SimpleHealthTracking.Repository.Factories
{
    using DTO;
    using Entities;

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
    }
}