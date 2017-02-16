namespace SimpleHealthTracking.Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Workout
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Workout Type")]
        public int WorkoutTypeId { get; set; }

        [StringLength(128)]
        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime DateAddedFor { get; set; }

        [StringLength(5000)]
        public string Notes { get; set; }

        [StringLength(5000)]
        [Display(Name = "Key Takeaways")]
        public string KeyTakeaways { get; set; }

        [Display(Name = "Pre Feeling")]
        public float? PreFeeling { get; set; }

        [Display(Name = "Post Feeling")]
        public float? PostFeeling { get; set; }

        [Display(Name = "Difficulty")]
        public float? DifficultyLevel { get; set; }

        [Display(Name="Length of Workout")]
        public float? LengthInMinutes { get; set; }

        public DateTime TimeAdded { get; set; }
        public DateTime UpdateTime { get; set; }
        public virtual WorkoutType WorkoutType { get; set; }
    }
}