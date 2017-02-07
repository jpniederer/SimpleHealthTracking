namespace SimpleHealthTracking.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingWorkoutClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkoutTypeId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        DateAddedFor = c.DateTime(nullable: false),
                        Notes = c.String(),
                        KeyTakeaways = c.String(),
                        PreFeeling = c.Single(),
                        PostFeeling = c.Single(),
                        DifficultyLevel = c.Single(),
                        LengthInMinutes = c.Single(),
                        TimeAdded = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkoutTypes", t => t.WorkoutTypeId, cascadeDelete: true)
                .Index(t => t.WorkoutTypeId);
            
            CreateTable(
                "dbo.WorkoutTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        TimeAdded = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workouts", "WorkoutTypeId", "dbo.WorkoutTypes");
            DropIndex("dbo.Workouts", new[] { "WorkoutTypeId" });
            DropTable("dbo.WorkoutTypes");
            DropTable("dbo.Workouts");
        }
    }
}
