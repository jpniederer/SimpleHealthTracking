namespace SimpleHealthTracking.Repository.Factories
{
    public class MedicineTakenFactory
    {
        public MedicineTakenFactory() { }

        public DTO.MedicineTaken CreateMedicineTaken(Entities.MedicineTaken medicineTaken)
        {
            return new DTO.MedicineTaken()
            {
                Id = medicineTaken.Id,
                MedicineId = medicineTaken.MedicineId,
                DateAddedFor = medicineTaken.DateAddedFor,
                TimeAdded = medicineTaken.TimeAdded
            };
        }

        public Entities.MedicineTaken CreateMedicineTaken(DTO.MedicineTaken medicineTaken)
        {
            return new Entities.MedicineTaken()
            {
                Id = medicineTaken.Id,
                MedicineId = medicineTaken.MedicineId,
                DateAddedFor = medicineTaken.DateAddedFor,
                TimeAdded = medicineTaken.TimeAdded
            };
        }
    }
}