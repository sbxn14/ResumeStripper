﻿using MySql.Data.Entity;
using ResumeStripper.Models;
using ResumeStripper.Models.AccountModels;
using ResumeStripper.Models.Experiences;
using System.Data.Entity;

namespace ResumeStripper.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class StripperContext : DbContext
    {
        //passes the connection string saved in the web.config to the DbContext
        public StripperContext() : base("MySqlCon")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<StripperContext>());
        }

        public DbSet<CV> Cvs { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<EducationExperience> EducationExperiences { get; set; }
        public DbSet<SidelineExperience> SidelineExperiences { get; set; }
        public DbSet<CourseExperience> CourseExperiences { get; set; }
        public DbSet<Competence> Competences { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}