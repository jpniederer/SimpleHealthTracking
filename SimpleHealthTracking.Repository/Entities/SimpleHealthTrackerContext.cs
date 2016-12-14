namespace SimpleHealthTracking.Repository.Entities
{
    using System.Data.Entity;

    public partial class SimpleHealthTrackingContext : DbContext
    {
        public SimpleHealthTrackingContext() : base("name=SimpleHealthTrackingContext") { }

        public virtual DbSet<Checkin> Checkins { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<MedicineTaken> MedicineTakens { get; set; }
        public virtual DbSet<Sleep> Sleeps { get; set; }
        public virtual DbSet<PublicStatsPage> PublicStatsPages { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Checkin>();

            modelBuilder.Entity<Medicine>()
                .HasMany(m => m.MedicineTakens)
                .WithRequired(m => m.Medicine).WillCascadeOnDelete();
        }
    }
}