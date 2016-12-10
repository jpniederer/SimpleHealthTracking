namespace SimpleHealthTracking.Web.ViewModels
{
    using Classes;
    using Repository.Entities;
    using System.Collections.Generic;

    public class PublicStatsViewModel
    {
        public PublicStatsPage PublicStatsPage { get; private set; }
        public List<MedicineStats> MedicineStatsList { get; private set; }
        public HealthStatistics HealthStats { get; private set; }
        private List<Medicine> medication;

        public PublicStatsViewModel() { }

        public PublicStatsViewModel(PublicStatsPage psp, List<Medicine> medicines)
        {
            PublicStatsPage = psp;
            medication = medicines;
            SetupMedicineStats();
            SetupHealthStats();
        }

        private void SetupMedicineStats()
        {
            MedicineStatsList = MedicineStats.GetMedicineStats(medication);
        }

        private void SetupHealthStats()
        {
            HealthStats = new HealthStatistics(PublicStatsPage.UserId, true);
        }
    }
}