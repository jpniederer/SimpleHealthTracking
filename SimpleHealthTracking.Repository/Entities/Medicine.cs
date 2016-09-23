namespace SimpleHealthTracking.Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public partial class Medicine
    {
        public int Id { get; set; }

        [StringLength(128)]
        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        public int NumberOfTimesPerDay { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime TimeAdded { get; set; }

        public DateTime UpdateTime { get; set; }

        public virtual ICollection<MedicineTaken> MedicineTakens { get; set; }

        public Medicine()
        {
            MedicineTakens = new HashSet<MedicineTaken>();
        }

        public Medicine(string userId, string name, int numberOfTimesPerDay, bool isActive)
        {
            UserId = userId;
            Name = name;
            NumberOfTimesPerDay = numberOfTimesPerDay;
            IsActive = isActive;
            TimeAdded = DateTime.Now;
            UpdateTime = DateTime.Now;
            MedicineTakens = new HashSet<MedicineTaken>();
        }
    }
}