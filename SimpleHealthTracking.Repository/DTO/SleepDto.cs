namespace SimpleHealthTracking.Repository.DTO
{
    using System;

    public class SleepDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public float? SleepQuality { get; set; }
        public float? MinutesSlept { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime UpdateTime { get; set; }

        public string StartDateText { get; set; }
        public string StartTimeText { get; set; }
        public string EndDateText { get; set; }
        public string EndTimeText { get; set; }

        public void SetDates()
        {
            if (StartDateText != "" && StartTimeText != "")
            {
                StartTime = DateTime.Parse(string.Format("{0} {1}", StartDateText, StartTimeText));
            }
            
            if (EndDateText != "" && EndTimeText != "")
            {
                EndTime = DateTime.Parse(string.Format("{0} {1}", EndDateText, EndTimeText));
            }
        }
    }
}