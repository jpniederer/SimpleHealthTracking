namespace SimpleHealthTracking.Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PublicStatsPage
    {
        public int Id { get; set; }

        [StringLength(128)]
        [Required]
        public string UserId { get; set; }

        public bool IsVisible { get; set; }

        public DateTime TimeAdded { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
