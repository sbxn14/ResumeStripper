namespace ResumeStripper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUser2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Salt", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Salt", c => c.Binary());
        }
    }
}
