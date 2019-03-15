namespace ResumeStripper.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addedSimpleNDetailedViewLanguage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Language", "isSimple", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Language", "isSimple");
        }
    }
}