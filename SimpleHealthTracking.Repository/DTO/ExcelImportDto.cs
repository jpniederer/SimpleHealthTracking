namespace SimpleHealthTracking.Repository.DTO
{
    public class ExcelImportDto
    {
        public string State { get; set; }
        public string DateEntry { get; set; }
        public string TimeEntry { get; set; }
        public string Weight { get; set; }
        public string Heartrate { get; set; }
        public string Notes { get; set; }
        public string Medicine { get; set; }
        public string Mind { get; set; }
        public string Body { get; set; }
        public string Feel { get; set; }
        public string UserId { get; set; }
        public int MedicineId { get; set; }
    }
}