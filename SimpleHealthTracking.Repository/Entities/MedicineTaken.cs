namespace SimpleHealthTracking.Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class MedicineTaken
    {
        public int Id { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [Required]
        public DateTime DateAddedFor { get; set; }

        public DateTime TimeAdded { get; set; }

        public virtual Medicine Medicine { get; set; }

        public MedicineTaken() { }

        public MedicineTaken(int medicineId, DateTime dateAddedFor)
        {
            MedicineId = medicineId;
            DateAddedFor = dateAddedFor;
            TimeAdded = DateTime.Now;
        }
    }
}