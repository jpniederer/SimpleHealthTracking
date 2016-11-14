namespace SimpleHealthTracking.Web.Classes
{
    using System.Collections.Generic;
    using System.Linq;
    using Repository;
    using Repository.Entities;

    public class HealthStatistics
    {
        List<Checkin> checkins;
        List<MedicineTaken> medicinesTaken;
        List<Sleep> sleeps;
        ISimpleHealthTrackerRepository repository;
        string userId;

        public HealthStatistics(string user)
        {
            userId = user;
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());
        }

        public HealthStatistics(ISimpleHealthTrackerRepository repo, string user)
        {
            repository = repo;
            userId = user;
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
            double averageWeight = 0.0;

            if (checkins == null)
            {
                SetupCheckins();
            }

            averageWeight = checkins.Where(c => c.Weight != null).Average(c => (float)c.Weight);

            return averageWeight;
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
            double maxWeight = 0.0;

            if (checkins == null)
            {
                SetupCheckins();
            }

            maxWeight = checkins.Where(c => c.Weight != null).Max(c => (float)c.Weight);

            return maxWeight;
        }
    }
}