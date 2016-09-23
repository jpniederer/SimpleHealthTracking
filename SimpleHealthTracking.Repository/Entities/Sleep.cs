namespace SimpleHealthTracking.Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Sleep
    {
        public int Id { get; set; }

        [StringLength(128)]
        [Required]
        public string UserId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public float? SleepQuality { get; set; }

        public float? MinutesSlept { get; set; }

        public DateTime TimeAdded { get; set; }

        public DateTime UpdateTime { get; set; }

        public Sleep() { }

        public Sleep(string userId)
        {
            UserId = userId;
            TimeAdded = DateTime.Now;
            UpdateTime = DateTime.Now;
        }
    }
}