namespace SimpleHealthTracking.Repository.DTO
{
    using System;

    public class MedicineTakenFullDto
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public DateTime DateAddedFor { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}