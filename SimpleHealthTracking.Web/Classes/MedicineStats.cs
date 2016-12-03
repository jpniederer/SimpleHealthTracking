namespace SimpleHealthTracking.Web.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Repository;
    using Repository.Entities;

    public class MedicineStats
    {
        public Medicine Medicine { get; set; }
        public List<MedicineTaken> MedicinesTaken { get; }
        public int CompletionCount { get; private set; }
        public int TotalExpected { get; private set; }
        public double CompletionPercentage { get; private set; }
        public bool DoesMedicineHaveRecords { get; private set; }
        public Streak LongestStreak { get; private set; }
        public Streak CurrentStreak { get; private set; }
        ISimpleHealthTrackingRepository repository;

        public MedicineStats(Medicine medicine)
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackerContext());
            Medicine = medicine;
            MedicinesTaken = repository.GetMedicineTakenByMedicineId(Medicine.Id).ToList();

            if (MedicinesTaken != null && MedicinesTaken.Count > 0)
            {
                DoesMedicineHaveRecords = true;
                SetCompletionCount();
                SetExpectedTotal();
                SetCompletionPercentage();
                SetStreaks();
            }
            else
            {
                DoesMedicineHaveRecords = false;
            }
        }

        private void SetCompletionCount()
        {
            CompletionCount = MedicinesTaken.Count();
        }

        private void SetExpectedTotal()
        {
            DateTime firstDay = MedicinesTaken.Min(mt => mt.DateAddedFor);
            long dayCount = ((DateTime.Now.Ticks - firstDay.Ticks) / TimeSpan.TicksPerDay) + 1;
            TotalExpected = (int)dayCount * Medicine.NumberOfTimesPerDay;
        }

        private void SetCompletionPercentage()
        {
            CompletionPercentage = 100 * ((double)CompletionCount / TotalExpected);
        }

        private void SetStreaks()
        {
            StreakGenerator sg = new StreakGenerator(Medicine, MedicinesTaken);
            LongestStreak = sg.GetLongestStreak();
            CurrentStreak = sg.GetCurrentStreak();
        }
    }
}