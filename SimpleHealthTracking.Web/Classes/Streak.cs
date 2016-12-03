namespace SimpleHealthTracking.Web.Classes
{
    using System;

    public class Streak
    {
        public int DayCount { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Streak(int dayCount, DateTime startDate, DateTime endDate)
        {
            DayCount = dayCount;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}