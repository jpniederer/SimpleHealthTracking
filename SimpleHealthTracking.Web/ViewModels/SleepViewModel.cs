namespace SimpleHealthTracking.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SleepViewModel
    {
        [Required]
        [ValidDate]
        public string StartDate { get; set; }

        [Required]
        [ValidTime]
        public string StartTime { get; set; }

        [Required]
        [ValidDate]
        public string EndDate { get; set; }

        [Required]
        [ValidTime]
        public string EndTime { get; set; }

        public string SleepQuality { get; set; }

        public DateTime GetStartDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", StartDate, StartTime));
        }

        public DateTime GetEndDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", EndDate, EndTime));
        }

        public float? GetSleepQuality()
        {
            float val;

            if (float.TryParse(SleepQuality, out val))
            {
                return val;
            }
            else
            {
                return null;
            }
        }
    }
}