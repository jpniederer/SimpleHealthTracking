namespace SimpleHealthTracking.Repository.DTO
{
    public class MedicineTakenNotificationDto
    {
        public string MedicineName { get; set; }
        public int NumberCompleted { get; set; }
        public int NumberNeeded { get; set; }
    }
}