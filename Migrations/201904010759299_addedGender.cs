namespace ResumeStripper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedGender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CV", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CV", "Gender");
        }
    }
}
