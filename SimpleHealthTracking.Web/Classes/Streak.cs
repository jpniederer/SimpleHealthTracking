namespace SimpleHealthTracking.Web.Classes
{
    using System;
    using System.Text;

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

        public string BuildDateString()
        {
            StringBuilder sb = new StringBuilder();

            if (DayCount > 0)
            {
                sb.Append("(").Append(StartDate.Date.ToShortDateString());
                sb.Append(" to ").Append(EndDate.Date.ToShortDateString());
                sb.Append(")");
            }

            return sb.ToString();
        }
    }
}