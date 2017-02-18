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
            DateTime currentStreakStartDate = DateTime.MinValue;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;

            foreach (DateTime day in EachDay(firstDate, DateTime.Now.Date.AddDays(-1)))
            {
                if (IsSatisfiedForDay(day))
                {
                    currentStreakStartDate = currentStreak == 0 ? day : currentStreakStartDate;
                    currentStreak++;

                    if (currentStreak > maxStreak)
                    {
                        startDate = currentStreakStartDate;
                        maxStreak = currentStreak;
                        endDate = day;
                    }
                }
                else
                {
                    currentStreak = 0;
                }
            }

            HandleCurrentDay(ref currentStreak, ref maxStreak, ref endDate);

            return new Streak(maxStreak, startDate, endDate);
        }

        public Streak GetCurrentStreak()
        {
            int maxStreak = 0;
            int currentStreak = 0;
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.MinValue;
            HandleCurrentDay(ref currentStreak, ref maxStreak, ref endDate);

            foreach (DateTime day in EachDayBackwards(DateTime.Now.Date.AddDays(-1), firstDate))
            {
                if (IsSatisfiedForDay(day))
                {
                    currentStreak++;
                    startDate = day;
                    endDate = currentStreak == 1 ? day : endDate;
                }
                else
                {
                    break;
                }
             }

            return new Streak(currentStreak, startDate, endDate);
        }

        // Only counts the current day for a streak if it's complete.
        public void HandleCurrentDay(ref int currentStreak, ref int maxStreak, ref DateTime endDate)
        {
            if (IsSatisfiedForDay(DateTime.Now.Date))
            {
                if (currentStreak == maxStreak)
                {
                    currentStreak++;
                    maxStreak++;
                    endDate = DateTime.Now.Date;
                }
            }
        }

        private bool IsSatisfiedForDay(DateTime date)
        {
            if (!dateCounts.ContainsKey(date.Date) || dateCounts[date.Date] < medicine.NumberOfTimesPerDay)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public IEnumerable<DateTime> EachDayBackwards(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date >= to.Date; day = day.AddDays(-1))
                yield return day;
        }
    }
}