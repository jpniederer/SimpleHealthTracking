namespace SimpleHealthTracking.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WorkoutViewModel
    {
        [Required]
        [Display(Name = "Workout Type")]
        public int WorkoutTypeId { get; set; }

        [ValidDateOrNull]
        [Display(Name = "Date")]
        public string DateOfString { get; set; }

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

        [Display(Name = "Length of Workout")]
        public float? LengthInMinutes { get; set; }

        public DateTime GetDateAddedFor()
        {
            DateTime date;
            
            if (DateTime.TryParse(DateOfString, out date))
            {
                return date;
            }

            return DateTime.Now;
        }

    }
}