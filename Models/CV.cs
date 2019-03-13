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
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(10)]
        public string Prefix { get; set; }
        [MaxLength(100)]
        [Required]
        public string Surname { get; set; }
        [Required]
        [MaxLength(20)]
        public string Residence { get; set; }
        [Required]
        [MaxLength(20)]
        public string Country { get; set; }
        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        //List of license enum, since a person can have multiple licenses, can be null
        public virtual List<License> Licenses { get; set; }
        [MaxLength(20000)]
        public string Profile { get; set; }
        public virtual List<EducationExperience> Educations { get; set; }
        public virtual List<WorkExperience> WorkExperiences { get; set; }
        public virtual List<CourseExperience> Courses { get; set; }
        public virtual List<Language> Languages { get; set; }
        public virtual List<Competence> Competences { get; set; }
        public virtual List<Hobby> Hobbies { get; set; }
        public virtual List<SidelineExperience> SideLines { get; set; }
        public virtual List<Reference> References { get; set; }
        public virtual List<Skill> Skills { get; set; }

        [NotMapped]
        public bool IsAnonymous { get; set; }

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

        public void setAnonymousCV()
        {
            //sets bool to true
            IsAnonymous = true;
        }
    }
}