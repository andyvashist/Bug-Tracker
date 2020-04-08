namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsSelectedToDevelopersInDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Developers", "IsSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Developers", "IsSelected");
        }
    }
}
