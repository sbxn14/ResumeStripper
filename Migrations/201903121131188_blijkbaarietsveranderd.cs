namespace ResumeStripper.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class blijkbaarietsveranderd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CV", "Name", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.CV", "Surname", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.CV", "Profile", c => c.String(maxLength: 20000, storeType: "nvarchar"));
        }

        public override void Down()
        {
            AlterColumn("dbo.CV", "Profile", c => c.String(maxLength: 50000, storeType: "nvarchar"));
            AlterColumn("dbo.CV", "Surname", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.CV", "Name", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
        }
    }
}