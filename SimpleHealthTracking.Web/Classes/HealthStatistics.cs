namespace SimpleHealthTracking.Web.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Repository;
    using Repository.Entities;

    public class HealthStatistics
    {
        public double AverageWeight { get; private set; }
        public Checkin MaxWeightCheckin { get; private set; }
        public Checkin MinWeightCheckin { get; private set; }
        public double AverageHeartrate { get; private set; }
        public Checkin MaxHeartrateCheckin { get; private set; }
        public Checkin MinHeartrateCheckin { get; private set; }
        public double AverageMinutesSlept { get; private set; }
        public DateTime AverageSleepStartTime { get; private set; }
        public DateTime AverageSleepEndTime { get; private set; }
        public Sleep MostSleep { get; private set; }
        public Sleep LeastSleep { get; private set; }
        List<Checkin> checkins;
        List<MedicineTaken> medicinesTaken;
        List<Sleep> sleeps;
        ISimpleHealthTrackingRepository repository;
        string userId;

        public HealthStatistics(string user, bool getAllStats = false)
        {
            userId = user;
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackerContext());

            if (getAllStats)
            {
                SetAllStats();
            }
        }

        public HealthStatistics(ISimpleHealthTrackingRepository repo, string user, bool getAllStats = false)
        {
            repository = repo;
            userId = user;

            if (getAllStats)
            {
                SetAllStats();
            }
        }

        private void SetAllStats()
        {
            GetAverageWeight();
            GetMaxWeight();
            GetMinWeight();
            GetAverageHeartrate();
            GetMaxHeartrate();
            GetMinHeartrate();
            GetAverageMinutesSlept();
            GetMostSleep();
            GetLeastSleep();
            SetAverageStartAndEndSleepTimes();
        }

        private bool SetupCheckins()
        {
            checkins = repository.GetCheckinsForUser(userId).ToList();

            if (checkins.Count == 0)
            {
                return false;
            }

            return true;
        }

        private bool SetupMedicinesTakens()
        {
            medicinesTaken = repository.GetMedicineTakenByUser(userId).ToList();

            if (medicinesTaken.Count == 0)
            {
                return false;
            }

            return true;
        }

        private bool SetupSleeps()
        {
            sleeps = repository.GetSleepForUser(userId).ToList();

            if (sleeps.Count == 0)
            {
                return false;
            }

            return true;
        }

        public double GetAverageWeight()
        {
            AverageWeight = 0.0;

            if (checkins == null || checkins.Count == 0)
            {
                if (!SetupCheckins())
                {
                    return AverageWeight;
                }
            }

            AverageWeight = checkins.Where(c => c.Weight != null).Average(c => (float)c.Weight);

            return AverageWeight;
        }

        public double GetAverageWeightFromLatestNumberOfEntries(int numberEntries)
        {
            double averageWeight = 0.0;

            if (checkins == null || checkins.Count == 0)
            {
                if (!SetupCheckins())
                {
                    return averageWeight;
                }
            }

            averageWeight = checkins.Where(c => c.Weight != null).Take(numberEntries)
                            .Average(c => (float)c.Weight);

            return averageWeight;
        }

        public Checkin GetMaxWeight()
        {
            MaxWeightCheckin = null;

            if (checkins == null || checkins.Count == 0)
            {
                if (!SetupCheckins())
                {
                    return null;
                }
            }

            MaxWeightCheckin = checkins.Where(c => c.Weight != null).OrderByDescending(c => c.Weight).Take(1).ToList()[0];

            return MaxWeightCheckin;
        }

        public Checkin GetMinWeight()
        {
            MinWeightCheckin = null;

            if (checkins == null || checkins.Count == 0)
            {
                if (!SetupCheckins())
                {
                    return null;
                }
            }

            MinWeightCheckin = checkins.Where(c => c.Weight != null).OrderBy(c => c.Weight).Take(1).ToList()[0];

            return MinWeightCheckin;
        }

        public double GetAverageHeartrate()
        {
            AverageHeartrate = 0.0;

            if (checkins == null || checkins.Count == 0)
            {
                if (!SetupCheckins())
                {
                    return AverageHeartrate;
                }
            }

            AverageHeartrate = checkins.Where(c => c.Heartrate != null).Average(c => (float)c.Heartrate);

            return AverageHeartrate;
        }

        public Checkin GetMaxHeartrate()
        {
            MaxHeartrateCheckin = null;

            if (checkins == null || checkins.Count == 0)
            {
                if (!SetupCheckins())
                {
                    return null;
                }
            }

            var maxHeartrateList = checkins.Where(c => c.Heartrate != null).OrderByDescending(c => (float)c.Heartrate).ToList();
            
            if (maxHeartrateList.Count > 0)
            {
                MaxHeartrateCheckin = maxHeartrateList[0];
            }

            return MaxHeartrateCheckin;
        }

        public Checkin GetMinHeartrate()
        {
            MinHeartrateCheckin = null;

            if (checkins == null || checkins.Count == 0)
            {
                if (!SetupCheckins())
                {
                    return null;
                }
            }

            var minHeartrateList = checkins.Where(c => c.Heartrate != null).OrderBy(c => c.Heartrate).ToList();

            if (minHeartrateList.Count > 0)
            {
                MinHeartrateCheckin = minHeartrateList[0];
            }

            return MinHeartrateCheckin;
        }

        public double GetAverageMinutesSlept()
        {
            AverageMinutesSlept = 0.0;

            if (sleeps == null || sleeps.Count == 0)
            {
                if (!SetupSleeps())
                {
                    return AverageMinutesSlept;
                }
            }

            AverageMinutesSlept = sleeps.Where(s => s.MinutesSlept != null).Average(s => (float)s.MinutesSlept);

            return AverageMinutesSlept;
        }

        public Sleep GetMostSleep()
        {
            MostSleep = null;

            if (sleeps == null || sleeps.Count == 0)
            {
                if (!SetupSleeps())
                {
                    return null;
                }
            }

            var mostSleepList = sleeps.Where(s => s.MinutesSlept != null).OrderByDescending(s => s.MinutesSlept).Take(1).ToList();

            if (mostSleepList.Count > 0)
            {
                MostSleep = mostSleepList[0];
            }

            return MostSleep;
        }

        public Sleep GetLeastSleep()
        {
            LeastSleep = null;

            if (sleeps == null || sleeps.Count == 0)
            {
                if (!SetupSleeps())
                {
                    return null;
                }
            }

            var leastSleepList = sleeps.Where(s => s.MinutesSlept != null).OrderBy(s => s.MinutesSlept).Take(1).ToList();

            if (leastSleepList.Count > 0)
            {
                LeastSleep = leastSleepList[0];
            }

            return LeastSleep;
        }

        public void SetAverageStartAndEndSleepTimes()
        {
            int totalStartMinutes = 0;
            int totalEndMinutes = 0;

            if (sleeps == null || sleeps.Count == 0)
            {
                if (!SetupSleeps())
                {
                    AverageSleepStartTime = DateTime.MinValue;
                    AverageSleepEndTime = DateTime.MinValue;
                    return;
                }
            }

            var wellDefinedSleeps = sleeps.Where(s => s.StartTime != null && s.EndTime != null).ToList();

            foreach (var sleep in wellDefinedSleeps)
            {
                if (sleep.StartTime.Value.Hour == 0)
                {
                    totalStartMinutes += 24 * 60 + sleep.StartTime.Value.Minute;
                }
                else if (sleep.StartTime.Value.Hour == 1)
                {
                    totalStartMinutes += 25 * 60 + sleep.StartTime.Value.Minute;
                }
                else if (sleep.StartTime.Value.Hour == 2)
                {
                    totalStartMinutes += 26 * 60 + sleep.StartTime.Value.Minute;
                }
                else
                {
                    totalStartMinutes += sleep.StartTime.Value.Hour * 60 + sleep.StartTime.Value.Minute;
                }
                
                totalEndMinutes += sleep.EndTime.Value.Hour * 60 + sleep.EndTime.Value.Minute;
            }

            int averageStartHour = totalStartMinutes / 60 / wellDefinedSleeps.Count;
            int averageStartMinute = totalStartMinutes / wellDefinedSleeps.Count % 60;
            int averageEndHour = totalEndMinutes / 60 / wellDefinedSleeps.Count;
            int averageEndMinute = totalEndMinutes / wellDefinedSleeps.Count % 60;

            AverageSleepStartTime = new DateTime(2016, 11, 16, averageStartHour % 24, averageStartMinute, 0, 0);
            AverageSleepEndTime = new DateTime(2016, 11, 16, averageEndHour, averageEndMinute, 0, 0);
        }
    }
}