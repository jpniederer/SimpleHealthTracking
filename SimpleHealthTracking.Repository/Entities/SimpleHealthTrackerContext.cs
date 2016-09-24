namespace SimpleHealthTracking.Repository.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;

    public partial class SimpleHealthTrackerContext : DbContext
    {
        public SimpleHealthTrackerContext() : base("name=SimpleHealthTrackingContext") { }

        public virtual DbSet<Checkin> Checkins { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<MedicineTaken> MedicineTakens { get; set; }
        public virtual DbSet<Sleep> Sleeps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Checkin>();

            modelBuilder.Entity<Medicine>()
                .HasMany(m => m.MedicineTakens)
                .WithRequired(m => m.Medicine).WillCascadeOnDelete();


        }
    }
}