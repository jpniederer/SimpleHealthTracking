namespace SimpleHealthTracking.Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Workout
    {
        public int Id { get; set; }

        [Required]
        public int WorkoutTypeId { get; set; }

        [StringLength(128)]
        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime DateAddedFor { get; set; }

        [StringLength(5000)]
        public string Notes { get; set; }

        [StringLength(5000)]
        public string KeyTakeaways { get; set; }

        public float? PreFeeling { get; set; }
        public float? PostFeeling { get; set; }
        public float? DifficultyLevel { get; set; }
        public float? LengthInMinutes { get; set; }

        public DateTime TimeAdded { get; set; }
        public DateTime UpdateTime { get; set; }
        public virtual WorkoutType WorkoutType { get; set; }
    }
}