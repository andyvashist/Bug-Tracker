namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssociatedProjectsAndTickets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "ProjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "ProjectId");
            AddForeignKey("dbo.Tickets", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Tickets", new[] { "ProjectId" });
            DropColumn("dbo.Tickets", "ProjectId");
        }
    }
}
