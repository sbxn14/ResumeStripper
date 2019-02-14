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
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string Surname { get; set; }
        public string Residence { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<int> LicenseCategoryIds { get; set; }
        //List of license enum, since a person can have multiple licenses, can be null
        public virtual ICollection<License> LicenseCategory { get; set; }
        public string Profile { get; set; }
        public virtual ICollection<EducationExperience> Educations { get; set; }
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }
        public virtual ICollection<CourseExperience> Courses { get; set; }
        [ForeignKey("Language")]
        public ICollection<int> LanguageIds { get; set; }
        public virtual ICollection<Language> Languages { get; set; }
        public ICollection<string> Competences { get; set; }
        public ICollection<string> Hobbies { get; set; }
        public virtual ICollection<SidelineExperience> SideLines { get; set; }
        public virtual ICollection<Reference> References { get; set; }
        [ForeignKey("Skill")]
        public ICollection<int> SkillIds { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }

        public CV(string name, string prefix, string surname, string residence, string country, DateTime dateOfBirth, ICollection<License> licenseCategory, string profile, ICollection<EducationExperience> educations, ICollection<WorkExperience> workExperiences, ICollection<CourseExperience> courses, ICollection<int> languageIds, ICollection<Language> languages, ICollection<string> competences, ICollection<string> hobbies, ICollection<SidelineExperience> sideLines, ICollection<Reference> references, ICollection<int> skillIds, ICollection<Skill> skills)
        {
            Name = name;
            Prefix = prefix;
            Surname = surname;
            Residence = residence;
            Country = country;
            DateOfBirth = dateOfBirth;
            LicenseCategory = licenseCategory;
            Profile = profile;
            Educations = educations;
            WorkExperiences = workExperiences;
            Courses = courses;
            LanguageIds = languageIds;
            Languages = languages;
            Competences = competences;
            Hobbies = hobbies;
            SideLines = sideLines;
            References = references;
            SkillIds = skillIds;
            Skills = skills;
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