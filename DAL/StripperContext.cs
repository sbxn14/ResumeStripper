using MySql.Data.Entity;
using ResumeStripper.Models;
using ResumeStripper.Models.Experiences;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;

namespace ResumeStripper.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class StripperContext : DbContext
    {
        //passes the connectionstring saved in the web.config to the DbContext
        public StripperContext() : base("MySqlCon")
        {
            Database.SetInitializer<StripperContext>(new DropCreateDatabaseAlways<StripperContext>());
        }
        
        public DbSet<CV> CVS { get; set; }
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
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //prevents data to be saved as (For example) "Skills" instead of "Skill"
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}