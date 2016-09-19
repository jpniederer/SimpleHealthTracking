namespace SimpleHealthTracking.Repository.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SimpleHealthTrackerContext : DbContext
    {
        public SimpleHealthTrackerContext() : base("name=SimpleHealthTrackerContext") { }

        public virtual DbSet<Checkin> { get; set; }
        public virtual DbSet<Medicine> { get; set; }
    }

}