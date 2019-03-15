namespace ResumeStripper.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangedRequiredOfSomeProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CV", "Name", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.CV", "Surname", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
        }

        public override void Down()
        {
            AlterColumn("dbo.CV", "Surname", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.CV", "Name", c => c.String(maxLength: 100, storeType: "nvarchar"));
        }
    }
}