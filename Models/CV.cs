using ResumeStripper.Models.Enums;
using ResumeStripper.Models.Experiences;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models
{
    public class CV
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(10)]
        public string Prefix { get; set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }
        [Required]
        [StringLength(20)]
        public string Residence { get; set; }
        [Required]
        [StringLength(20)]
        public string Country { get; set; }
        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        //[ForeignKey("License")]
        //public List<int> LicenseCategoryIds { get; set; }
        //List of license enum, since a person can have multiple licenses, can be null
        public virtual List<License> Licenses { get; set; }
        public string Profile { get; set; }
        public virtual List<EducationExperience> Educations { get; set; }
        public virtual List<WorkExperience> WorkExperiences { get; set; }
        public virtual List<CourseExperience> Courses { get; set; }
        //[ForeignKey("Language")]
        //public List<int> LanguageIds { get; set; }
        public virtual List<Language> Languages { get; set; }
        public virtual List<Competence> Competences { get; set; }
        //[ForeignKey("Competence")]
        //public List<int> CompetenceIds { get; set; }
        public virtual List<Hobby> Hobbies { get; set; }
        //[ForeignKey("Hobby")]
        //public List<int> HobbyIds { get; set; }
        public virtual List<SidelineExperience> SideLines { get; set; }
        public virtual List<Reference> References { get; set; }
        //[ForeignKey("Skill")]
        //public List<int> SkillIds { get; set; }
        public virtual List<Skill> Skills { get; set; }

        public CV()
        {
            Licenses = new List<License>();
            Educations = new List<EducationExperience>();
            WorkExperiences = new List<WorkExperience>();
            Courses = new List<CourseExperience>();
            Languages = new List<Language>();
            Competences = new List<Competence>();
            Hobbies = new List<Hobby>();
            SideLines = new List<SidelineExperience>();
            References = new List<Reference>();
            Skills = new List<Skill>();
        }

        public CV GetAnonymousCV()
        {
            //creates a CV without the name of the person and returns it
            CV a = this;
            a.Name = String.Empty;
            a.Prefix = String.Empty;
            a.Surname = String.Empty;
            return a;
        }
    }
}