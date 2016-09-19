namespace SimpleHealthTracking.Repository.DTO
{
    using System;

    public class Sleep
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public float? SleepQuality { get; set; }
        public float? MinutesSlept { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}