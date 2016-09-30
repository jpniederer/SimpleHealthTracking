namespace SimpleHealthTracking.Repository.DTO
{
    using System;

    public class CheckinDto
    {
        public int Id { get; set; }
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
    }
}