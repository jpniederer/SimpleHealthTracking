namespace SimpleHealthTracking.Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Medicine
    {
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

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

        public Medicine() { }

        public Medicine(Guid userId, string name, int numberOfTimesPerDay, bool isActive)
        {
            UserId = userId;
            Name = name;
            NumberOfTimesPerDay = numberOfTimesPerDay;
            IsActive = isActive;
            TimeAdded = DateTime.Now;
            UpdateTime = DateTime.Now;
        }
    }
}