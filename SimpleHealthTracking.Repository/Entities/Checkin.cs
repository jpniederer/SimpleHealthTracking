namespace SimpleHealthTracking.Repository.Entities
{

    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Checkin
    {
        public int Id { get; set; }

        [StringLength(128)]
        [Required]
        public string UserId { get; set; }

        public float? Weight { get; set; }

        public float? Heartrate { get; set; }

        public float? SystolicBloodPressure { get; set; }

        public float? DiastolicBloodPressure { get; set; }

        public float? PhysicalFeelingRating { get; set; }

        public float? PsychologicalFeelingRating { get; set; }

        public float? ExerciseRating { get; set; }

        public string Notes { get; set; }

        public DateTime TimeAdded { get; set; }

        public DateTime UpdateTime { get; set; }

        public Checkin() { }

        public Checkin(string userId)
        {
            UserId = userId;
            TimeAdded = DateTime.Now;
            UpdateTime = DateTime.Now;
        }
    }
}