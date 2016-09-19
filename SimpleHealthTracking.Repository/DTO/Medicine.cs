namespace SimpleHealthTracking.Repository.DTO
{
    using System;

    public class Medicine
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int NumberOfTimesPerDay { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}