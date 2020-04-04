namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRealtionBetweenDeveloperAndProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectDevelopers",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        DeveloperId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.DeveloperId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Developers", t => t.DeveloperId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.DeveloperId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectDevelopers", "DeveloperId", "dbo.Developers");
            DropForeignKey("dbo.ProjectDevelopers", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectDevelopers", new[] { "DeveloperId" });
            DropIndex("dbo.ProjectDevelopers", new[] { "ProjectId" });
            DropTable("dbo.ProjectDevelopers");
        }
    }
}
