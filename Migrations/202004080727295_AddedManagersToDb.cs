namespace BugTracker.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedManagersToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Managers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(),
                    LastName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Projects", "ManagerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "ManagerId");
            AddForeignKey("dbo.Projects", "ManagerId", "dbo.Managers", "Id", cascadeDelete: false);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Projects", "ManagerId", "dbo.Managers");
            DropIndex("dbo.Projects", new[] { "ManagerId" });
            DropColumn("dbo.Projects", "ManagerId");
            DropTable("dbo.Managers");
        }
    }
}
