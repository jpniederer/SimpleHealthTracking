namespace SimpleHealthTracking.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class WorkoutType
    {
        public int Id { get; set; }

        [StringLength(128)]
        [Required]
        public string Name { get; set; }

        public DateTime TimeAdded { get; set; }
        public DateTime UpdateTime { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
    }
}