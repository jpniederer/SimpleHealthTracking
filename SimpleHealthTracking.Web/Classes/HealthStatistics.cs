namespace SimpleHealthTracking.Web.Classes
{
    using System.Collections.Generic;
    using System.Linq;
    using Repository;
    using Repository.Entities;

    public class HealthStatistics
    {
        public double AverageWeight { get; private set; }
        public double MaxWeight { get; private set; }
        public double MinWeight { get; private set; }
        public double AverageHeartrate { get; private set; }
        public double MaxHeartrate { get; private set; }
        public double MinHeartrate { get; private set; }
        List<Checkin> checkins;
        List<MedicineTaken> medicinesTaken;
        List<Sleep> sleeps;
        ISimpleHealthTrackerRepository repository;
        string userId;

        public HealthStatistics(string user, bool getAllStats = false)
        {
            userId = user;
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());

            if (getAllStats)
            {
                SetAllStats();
            }
        }

        public HealthStatistics(ISimpleHealthTrackerRepository repo, string user, bool getAllStats = false)
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
        }

        private void SetupCheckins()
        {
            checkins = repository.GetCheckinsForUser(userId).ToList();
        }

        private void SetupMedicinesTakens()
        {
            medicinesTaken = repository.GetMedicineTakenByUser(userId).ToList();
        }

        private void SetupSleeps()
        {
            sleeps = repository.GetSleepForUser(userId).ToList();
        }

        public double GetAverageWeight()
        {
            AverageWeight = 0.0;

            if (checkins == null)
            {
                SetupCheckins();
            }

            AverageWeight = checkins.Where(c => c.Weight != null).Average(c => (float)c.Weight);

            return AverageWeight;
        }

        public double GetAverageWeightFromLatestNumberOfEntries(int numberEntries)
        {
            double averageWeight = 0.0;

            if (checkins == null)
            {
                SetupCheckins();
            }

            averageWeight = checkins.Where(c => c.Weight != null).Take(numberEntries)
                            .Average(c => (float)c.Weight);

            return averageWeight;
        }

        public double GetMaxWeight()
        {
            MaxWeight = 0.0;

            if (checkins == null)
            {
                SetupCheckins();
            }

            MaxWeight = checkins.Where(c => c.Weight != null).Max(c => (float)c.Weight);

            return MaxWeight;
        }

        public double GetMinWeight()
        {
            MinWeight = 0.0;

            if (checkins == null)
            {
                SetupCheckins();
            }

            MinWeight = checkins.Where(c => c.Weight != null).Min(c => (float)c.Weight);

            return MinWeight;
        }

        public double GetAverageHeartrate()
        {
            AverageHeartrate = 0.0;

            if (checkins == null)
            {
                SetupCheckins();
            }

            AverageHeartrate = checkins.Where(c => c.Heartrate != null).Average(c => (float)c.Heartrate);

            return AverageHeartrate;
        }

        public double GetMaxHeartrate()
        {
            MaxHeartrate = 0.0;

            if (checkins == null)
            {
                SetupCheckins();
            }

            MaxHeartrate = checkins.Where(c => c.Heartrate != null).Max(c => (float)c.Heartrate);

            return MaxHeartrate;
        }

        public double GetMinHeartrate()
        {
            MinHeartrate = 0.0;

            if (checkins == null)
            {
                SetupCheckins();
            }

            MinHeartrate = checkins.Where(c => c.Heartrate != null).Min(c => (float)c.Heartrate);

            return MinHeartrate;
        }
    }
}