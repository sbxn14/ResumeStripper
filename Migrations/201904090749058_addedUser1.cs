namespace ResumeStripper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUser1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Location = c.String(unicode: false),
                        Sector = c.String(unicode: false),
                        Package = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Emailaddress = c.String(unicode: false),
                        Password = c.String(unicode: false),
                        Salt = c.Binary(),
                        Role = c.Int(nullable: false),
                        UserCompany_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.UserCompany_Id)
                .Index(t => t.UserCompany_Id);
            
            AddColumn("dbo.CVs", "Company_Id", c => c.Int());
            CreateIndex("dbo.CVs", "Company_Id");
            AddForeignKey("dbo.CVs", "Company_Id", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserCompany_Id", "dbo.Companies");
            DropForeignKey("dbo.CVs", "Company_Id", "dbo.Companies");
            DropIndex("dbo.Users", new[] { "UserCompany_Id" });
            DropIndex("dbo.CVs", new[] { "Company_Id" });
            DropColumn("dbo.CVs", "Company_Id");
            DropTable("dbo.Users");
            DropTable("dbo.Companies");
        }
    }
}
