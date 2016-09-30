namespace SimpleHealthTracking.Repository.DTO
{
    using System;
    using System.Collections.Generic;

    public class MedicineDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int NumberOfTimesPerDay { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime UpdateTime { get; set; }
        public ICollection<MedicineTakenFullDto> MedicineTakens { get; set; }
    }
}