namespace SimpleHealthTracking.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialRepositoryMode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Checkins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Weight = c.Single(),
                        Heartrate = c.Single(),
                        SystolicBloodPressure = c.Single(),
                        DiastolicBloodPressure = c.Single(),
                        PhysicalFeelingRating = c.Single(),
                        PsychologicalFeelingRating = c.Single(),
                        ExerciseRating = c.Single(),
                        Notes = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 500),
                        NumberOfTimesPerDay = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        TimeAdded = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicineTakens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicineId = c.Int(nullable: false),
                        DateAddedFor = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.Sleeps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        SleepQuality = c.Single(),
                        MinutesSlept = c.Single(),
                        TimeAdded = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicineTakens", "MedicineId", "dbo.Medicines");
            DropIndex("dbo.MedicineTakens", new[] { "MedicineId" });
            DropTable("dbo.Sleeps");
            DropTable("dbo.MedicineTakens");
            DropTable("dbo.Medicines");
            DropTable("dbo.Checkins");
        }
    }
}
