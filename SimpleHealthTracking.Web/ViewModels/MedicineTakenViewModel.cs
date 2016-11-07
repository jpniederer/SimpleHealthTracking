namespace SimpleHealthTracking.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MedicineTakenViewModel
    {
        [Required]
        public int MedicineId { get; set; }

        [Required]
        [ValidDate]
        public string DateAddedFor { get; set; }

        [Required]
        [ValidTime]
        public string TimeAddedFor { get; set; }

        public DateTime GetDateTimeAddedFor()
        {
            return DateTime.Parse(string.Format("{0} {1}", DateAddedFor, TimeAddedFor));
        }
    }
}