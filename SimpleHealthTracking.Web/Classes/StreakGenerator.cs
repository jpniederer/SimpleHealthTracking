namespace SimpleHealthTracking.Web.Classes
{
    using Repository.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StreakGenerator
    {
        private IEnumerable<MedicineTaken> medicinesTaken;
        private Medicine medicine;
        private Dictionary<DateTime, int> dateCounts = new Dictionary<DateTime, int>();
        private DateTime firstDate;

        public StreakGenerator(Medicine medicine, IEnumerable<MedicineTaken> medicineTaken)
        {
            this.medicine = medicine;
            medicinesTaken = medicineTaken;
            firstDate = medicinesTaken.Min(mt => mt.DateAddedFor.Date);
            SetupDateCounts();
        }

        private void SetupDateCounts()
        {
            foreach (var mt in medicinesTaken)
            {
                if (dateCounts.ContainsKey(mt.DateAddedFor.Date))
                {
                    dateCounts[mt.DateAddedFor.Date]++;
                }
                else
                {
                    dateCounts.Add(mt.DateAddedFor.Date, 1);
                }
            }
        }

        public Streak GetLongestStreak()
        {
            int maxStreak = 0;
            int currentStreak = 0;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;

            foreach (DateTime day in EachDay(firstDate, DateTime.Now.Date.AddDays(-1)))
            {
                if (IsSatisfiedForDay(day))
                {
                    currentStreak++;

                    startDate = maxStreak == 0 ? day : startDate;
                    if (currentStreak > maxStreak)
                    {
                        maxStreak = currentStreak;
                        endDate = day;
                    }
                }
                else
                {
                    currentStreak = 0;
                }
            }

            // Handle Current Day. Only count if it's complete.
            if (IsSatisfiedForDay(DateTime.Now.Date))
            {
                if (currentStreak == maxStreak && endDate.Date == DateTime.Now.Date.AddDays(-1))
                {
                    maxStreak++;
                    endDate = DateTime.Now.Date;
                }
            }

            return new Streak(maxStreak, startDate, endDate);
        }

        public Streak GetCurrentStreak()
        {
            int currentStreak = 0;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;

            foreach (DateTime day in EachDay(startDate, DateTime.Now.Date.AddDays(-1)))
            {
                
            }

            return new Streak(currentStreak, startDate, endDate);
        }

        private bool IsSatisfiedForDay(DateTime date)
        {
            if (!dateCounts.ContainsKey(date.Date))
            {
                return false;
            }
            if (dateCounts[date.Date] >= medicine.NumberOfTimesPerDay)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public IEnumerable<DateTime> EachDayBackwards(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(-1))
                yield return day;
        }
    }
}