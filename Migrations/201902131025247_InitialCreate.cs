namespace ResumeStripper.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CV",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(unicode: false),
                    Prefix = c.String(unicode: false),
                    Surname = c.String(unicode: false),
                    Residence = c.String(unicode: false),
                    Country = c.String(unicode: false),
                    DateOfBirth = c.DateTime(nullable: false, precision: 0),
                    Profile = c.String(unicode: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.CourseExperience",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(unicode: false),
                    Year = c.DateTime(nullable: false, precision: 0),
                    Certificate = c.Boolean(nullable: false),
                    CVID = c.Int(nullable: false),
                    OrganizationName = c.String(unicode: false),
                    LocationOrganization = c.String(unicode: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CV", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);

            CreateTable(
                "dbo.EducationExperience",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(unicode: false),
                    LevelOfEducation = c.String(unicode: false),
                    Diploma = c.Boolean(nullable: false),
                    BeginDate = c.DateTime(nullable: false, precision: 0),
                    EndDate = c.DateTime(nullable: false, precision: 0),
                    CVID = c.Int(nullable: false),
                    OrganizationName = c.String(unicode: false),
                    LocationOrganization = c.String(unicode: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CV", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);

            CreateTable(
                "dbo.Language",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(unicode: false),
                    Level = c.Int(nullable: false),
                    LevelOfListening = c.Int(nullable: false),
                    LevelOfSpeaking = c.Int(nullable: false),
                    LevelOfWriting = c.Int(nullable: false),
                    CV_ID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CV", t => t.CV_ID)
                .Index(t => t.CV_ID);

            CreateTable(
                "dbo.Reference",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    CVID = c.Int(nullable: false),
                    Name = c.String(unicode: false),
                    CompanyName = c.String(unicode: false),
                    JobTitle = c.String(unicode: false),
                    Email = c.String(unicode: false),
                    PhoneNumber = c.String(unicode: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CV", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);

            CreateTable(
                "dbo.SidelineExperience",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    JobTitle = c.String(unicode: false),
                    TaskDescription = c.String(unicode: false),
                    BeginDate = c.DateTime(nullable: false, precision: 0),
                    EndDate = c.DateTime(nullable: false, precision: 0),
                    CVID = c.Int(nullable: false),
                    OrganizationName = c.String(unicode: false),
                    LocationOrganization = c.String(unicode: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CV", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);

            CreateTable(
                "dbo.Skill",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(unicode: false),
                    CV_ID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CV", t => t.CV_ID)
                .Index(t => t.CV_ID);

            CreateTable(
                "dbo.WorkExperience",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    JobTitle = c.String(unicode: false),
                    TaskDescription = c.String(unicode: false),
                    BeginDate = c.DateTime(nullable: false, precision: 0),
                    EndDate = c.DateTime(nullable: false, precision: 0),
                    CVID = c.Int(nullable: false),
                    OrganizationName = c.String(unicode: false),
                    LocationOrganization = c.String(unicode: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CV", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.WorkExperience", "CVID", "dbo.CV");
            DropForeignKey("dbo.Skill", "CV_ID", "dbo.CV");
            DropForeignKey("dbo.SidelineExperience", "CVID", "dbo.CV");
            DropForeignKey("dbo.Reference", "CVID", "dbo.CV");
            DropForeignKey("dbo.Language", "CV_ID", "dbo.CV");
            DropForeignKey("dbo.EducationExperience", "CVID", "dbo.CV");
            DropForeignKey("dbo.CourseExperience", "CVID", "dbo.CV");
            DropIndex("dbo.WorkExperience", new[] { "CVID" });
            DropIndex("dbo.Skill", new[] { "CV_ID" });
            DropIndex("dbo.SidelineExperience", new[] { "CVID" });
            DropIndex("dbo.Reference", new[] { "CVID" });
            DropIndex("dbo.Language", new[] { "CV_ID" });
            DropIndex("dbo.EducationExperience", new[] { "CVID" });
            DropIndex("dbo.CourseExperience", new[] { "CVID" });
            DropTable("dbo.WorkExperience");
            DropTable("dbo.Skill");
            DropTable("dbo.SidelineExperience");
            DropTable("dbo.Reference");
            DropTable("dbo.Language");
            DropTable("dbo.EducationExperience");
            DropTable("dbo.CourseExperience");
            DropTable("dbo.CV");
        }
    }
}