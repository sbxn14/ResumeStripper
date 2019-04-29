using MySql.Data.Entity;
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
        public StripperContext() : base("testCon")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<StripperContext>());
        }

        public IDbSet<CV> Cvs { get; set; }
        public IDbSet<WorkExperience> WorkExperiences { get; set; }
        public IDbSet<EducationExperience> EducationExperiences { get; set; }
        public IDbSet<SidelineExperience> SidelineExperiences { get; set; }
        public IDbSet<CourseExperience> CourseExperiences { get; set; }
        public IDbSet<Competence> Competences { get; set; }
        public IDbSet<Skill> Skills { get; set; }
        public IDbSet<License> Licenses { get; set; }
        public IDbSet<Reference> References { get; set; }
        public IDbSet<Language> Languages { get; set; }
        public IDbSet<Hobby> Hobbies { get; set; }
        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<Company> Companies { get; set; }
    }
}