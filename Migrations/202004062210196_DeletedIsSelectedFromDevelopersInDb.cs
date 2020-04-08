namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedIsSelectedFromDevelopersInDb : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Developers", "IsSelected");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Developers", "IsSelected", c => c.Boolean(nullable: false));
        }
    }
}
