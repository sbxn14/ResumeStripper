namespace ResumeStripper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Competences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        CvID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CVs", t => t.CvID, cascadeDelete: true)
                .Index(t => t.CvID);
            
            CreateTable(
                "dbo.CVs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Prefix = c.String(maxLength: 100, storeType: "nvarchar"),
                        Surname = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Gender = c.Int(nullable: false),
                        Residence = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Country = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        DateOfBirth = c.DateTime(nullable: false, precision: 0),
                        Profile = c.String(maxLength: 20000, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseExperiences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Year = c.DateTime(nullable: false, precision: 0),
                        Certificate = c.Boolean(nullable: false),
                        CVID = c.Int(nullable: false),
                        OrganizationName = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        LocationOrganization = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CVs", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);
            
            CreateTable(
                "dbo.EducationExperiences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        LevelOfEducation = c.String(nullable: false, unicode: false),
                        Diploma = c.Boolean(nullable: false),
                        BeginDate = c.DateTime(nullable: false, precision: 0),
                        EndDate = c.DateTime(nullable: false, precision: 0),
                        CVID = c.Int(nullable: false),
                        OrganizationName = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        LocationOrganization = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CVs", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);
            
            CreateTable(
                "dbo.Hobbies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        CVID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CVs", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Level = c.Int(nullable: false),
                        LevelOfListening = c.Int(nullable: false),
                        LevelOfSpeaking = c.Int(nullable: false),
                        LevelOfWriting = c.Int(nullable: false),
                        IsSimple = c.Boolean(nullable: false),
                        CV_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CVs", t => t.CV_ID)
                .Index(t => t.CV_ID);
            
            CreateTable(
                "dbo.Licenses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.References",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CVID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        CompanyName = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        JobTitle = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                        PhoneNumber = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CVs", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);
            
            CreateTable(
                "dbo.SidelineExperiences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        TaskDescription = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                        BeginDate = c.DateTime(nullable: false, precision: 0),
                        EndDate = c.DateTime(nullable: false, precision: 0),
                        CVID = c.Int(nullable: false),
                        OrganizationName = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        LocationOrganization = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CVs", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        CVID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CVs", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);
            
            CreateTable(
                "dbo.WorkExperiences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        TaskDescription = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                        BeginDate = c.DateTime(nullable: false, precision: 0),
                        EndDate = c.DateTime(nullable: false, precision: 0),
                        CVID = c.Int(nullable: false),
                        OrganizationName = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        LocationOrganization = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CVs", t => t.CVID, cascadeDelete: true)
                .Index(t => t.CVID);
            
            CreateTable(
                "dbo.LicenseCVs",
                c => new
                    {
                        License_ID = c.Int(nullable: false),
                        CV_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.License_ID, t.CV_ID })
                .ForeignKey("dbo.Licenses", t => t.License_ID, cascadeDelete: true)
                .ForeignKey("dbo.CVs", t => t.CV_ID, cascadeDelete: true)
                .Index(t => t.License_ID)
                .Index(t => t.CV_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Competences", "CvID", "dbo.CVs");
            DropForeignKey("dbo.WorkExperiences", "CVID", "dbo.CVs");
            DropForeignKey("dbo.Skills", "CVID", "dbo.CVs");
            DropForeignKey("dbo.SidelineExperiences", "CVID", "dbo.CVs");
            DropForeignKey("dbo.References", "CVID", "dbo.CVs");
            DropForeignKey("dbo.LicenseCVs", "CV_ID", "dbo.CVs");
            DropForeignKey("dbo.LicenseCVs", "License_ID", "dbo.Licenses");
            DropForeignKey("dbo.Languages", "CV_ID", "dbo.CVs");
            DropForeignKey("dbo.Hobbies", "CVID", "dbo.CVs");
            DropForeignKey("dbo.EducationExperiences", "CVID", "dbo.CVs");
            DropForeignKey("dbo.CourseExperiences", "CVID", "dbo.CVs");
            DropIndex("dbo.LicenseCVs", new[] { "CV_ID" });
            DropIndex("dbo.LicenseCVs", new[] { "License_ID" });
            DropIndex("dbo.WorkExperiences", new[] { "CVID" });
            DropIndex("dbo.Skills", new[] { "CVID" });
            DropIndex("dbo.SidelineExperiences", new[] { "CVID" });
            DropIndex("dbo.References", new[] { "CVID" });
            DropIndex("dbo.Languages", new[] { "CV_ID" });
            DropIndex("dbo.Hobbies", new[] { "CVID" });
            DropIndex("dbo.EducationExperiences", new[] { "CVID" });
            DropIndex("dbo.CourseExperiences", new[] { "CVID" });
            DropIndex("dbo.Competences", new[] { "CvID" });
            DropTable("dbo.LicenseCVs");
            DropTable("dbo.WorkExperiences");
            DropTable("dbo.Skills");
            DropTable("dbo.SidelineExperiences");
            DropTable("dbo.References");
            DropTable("dbo.Licenses");
            DropTable("dbo.Languages");
            DropTable("dbo.Hobbies");
            DropTable("dbo.EducationExperiences");
            DropTable("dbo.CourseExperiences");
            DropTable("dbo.CVs");
            DropTable("dbo.Competences");
        }
    }
}
