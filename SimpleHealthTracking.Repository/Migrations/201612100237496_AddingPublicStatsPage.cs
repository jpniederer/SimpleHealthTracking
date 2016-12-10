namespace SimpleHealthTracking.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPublicStatsPage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublicStatsPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IsVisible = c.Boolean(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Medicines", "IsPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medicines", "IsPublic");
            DropTable("dbo.PublicStatsPages");
        }
    }
}
