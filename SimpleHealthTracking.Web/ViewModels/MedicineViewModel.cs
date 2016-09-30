namespace SimpleHealthTracking.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class MedicineViewModel
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public int NumberOfTimesPerDay { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [ValidDateOrNull]
        public string StartDate { get; set; }

        [ValidDateOrNull]
        public string EndDate { get; set; }

        public DateTime? GetStartDate()
        {
            DateTime startDate;

            if (DateTime.TryParse(StartDate, out startDate))
            {
                return startDate;
            }

            return null;
        }

        public DateTime? GetEndDate()
        {
            DateTime endDate;

            if (DateTime.TryParse(EndDate, out endDate))
            {
                return endDate;
            }

            return null;
        }
    }
}