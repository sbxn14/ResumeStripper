using ResumeStripper.Models.AccountModels;
using ResumeStripper.Models.Enums;
using ResumeStripper.Models.Experiences;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models
{
    public class CV
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be atleast 2 characters!")]
        public string Name { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string Prefix { get; set; }

        [Required(ErrorMessage = "Surname is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Surname should be atleast 2 characters!")]
        public string Surname { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Residence is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Residence should be atleast 2 characters!")]
        public string Residence { get; set; }

        [Required(ErrorMessage = "Country is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Country should be atleast 2 characters!")]
        public string Country { get; set; }

        [Column("DateOfBirth")]
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        //List of license enum, since a person can have multiple licenses, can be null
        public virtual List<License> Licenses { get; set; }

        [StringLength(20000, ErrorMessage = "Profile is too long, it can only be 20000 characters!")]
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

        //to see which company made this CV
        public virtual Company Company { get; set; }

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

        public void SetAnonymousCv()
        {
            //sets bool to true
            IsAnonymous = true;
        }

        public void TrimEverything()
        {
            //removes any accidental whitespaces at beginning and end of each string
            if (!string.IsNullOrEmpty(Name)) Name = Name.Trim();
            if (!string.IsNullOrEmpty(Prefix)) Prefix = Prefix.Trim();
            if (!string.IsNullOrEmpty(Surname)) Surname = Surname.Trim();
            if (!string.IsNullOrEmpty(Residence)) Residence = Residence.Trim();
            if (!string.IsNullOrEmpty(Country)) Country = Country.Trim();
            if (!string.IsNullOrEmpty(Profile)) Profile = Profile.Trim();

            if (Educations.Count > 0)
            {
                foreach (EducationExperience e in Educations)
                {
                    e.Name = e.Name.Trim();
                    e.LevelOfEducation = e.LevelOfEducation.Trim();
                    e.OrganizationName = e.OrganizationName.Trim();
                    e.LocationOrganization = e.LocationOrganization.Trim();
                }
            }

            if (Courses.Count > 0)
            {
                foreach (CourseExperience e in Courses)
                {
                    e.Name = e.Name.Trim();
                    e.OrganizationName = e.OrganizationName.Trim();
                    e.LocationOrganization = e.LocationOrganization.Trim();
                }
            }

            if (WorkExperiences.Count > 0)
            {
                foreach (WorkExperience e in WorkExperiences)
                {
                    e.JobTitle = e.JobTitle.Trim();
                    e.TaskDescription = e.TaskDescription.Trim();
                    e.OrganizationName = e.OrganizationName.Trim();
                    e.LocationOrganization = e.LocationOrganization.Trim();
                }
            }

            if (SideLines.Count > 0)
            {
                foreach (SidelineExperience e in SideLines)
                {
                    e.JobTitle = e.JobTitle.Trim();
                    e.TaskDescription = e.TaskDescription.Trim();
                    e.OrganizationName = e.OrganizationName.Trim();
                    e.LocationOrganization = e.LocationOrganization.Trim();
                }
            }

            if (References.Count > 0)
            {
                foreach (Reference e in References)
                {
                    e.JobTitle = e.JobTitle.Trim();
                    e.Name = e.Name.Trim();
                    e.CompanyName = e.CompanyName.Trim();
                }
            }

            if (Skills.Count > 0)
            {
                foreach (Skill e in Skills)
                {
                    e.Name = e.Name.Trim();
                }
            }

            if (Hobbies.Count > 0)
            {
                foreach (Hobby e in Hobbies)
                {
                    e.Name = e.Name.Trim();
                }
            }

            if (Competences.Count > 0)
            {
                foreach (Competence e in Competences)
                {
                    e.Name = e.Name.Trim();
                }
            }
        }

        public void CapitalizeLists()
        {
            if (Educations.Count > 0)
            {
                foreach (EducationExperience e in Educations)
                {
                    e.Name = CapitalizeFirstLetter(e.Name);
                    e.LevelOfEducation = CapitalizeFirstLetter(e.LevelOfEducation);
                    e.OrganizationName = CapitalizeFirstLetter(e.OrganizationName);
                    e.LocationOrganization = CapitalizeFirstLetter(e.LocationOrganization);
                }
            }

            if (WorkExperiences.Count > 0)
            {
                foreach (WorkExperience e in WorkExperiences)
                {
                    e.JobTitle = CapitalizeFirstLetter(e.JobTitle);
                    e.TaskDescription = CapitalizeFirstLetter(e.TaskDescription);
                    e.OrganizationName = CapitalizeFirstLetter(e.OrganizationName);
                    e.LocationOrganization = CapitalizeFirstLetter(e.LocationOrganization);
                }
            }

            if (Courses.Count > 0)
            {
                foreach (CourseExperience e in Courses)
                {
                    e.Name = CapitalizeFirstLetter(e.Name);
                    e.OrganizationName = CapitalizeFirstLetter(e.OrganizationName);
                    e.LocationOrganization = CapitalizeFirstLetter(e.LocationOrganization);
                }
            }

            if (SideLines.Count > 0)
            {
                foreach (SidelineExperience e in SideLines)
                {
                    e.JobTitle = CapitalizeFirstLetter(e.JobTitle);
                    e.TaskDescription = CapitalizeFirstLetter(e.TaskDescription);
                    e.OrganizationName = CapitalizeFirstLetter(e.OrganizationName);
                    e.LocationOrganization = CapitalizeFirstLetter(e.LocationOrganization);
                }
            }

            if (References.Count > 0)
            {
                foreach (Reference e in References)
                {
                    e.Name = CapitalizeFirstLetter(e.Name);
                    e.CompanyName = CapitalizeFirstLetter(e.CompanyName);
                    e.JobTitle = CapitalizeFirstLetter(e.JobTitle);
                }
            }
        }

        public string CapitalizeFirstLetter(string s)
        {
            int position = 0;
            
            char[] a = s.ToCharArray();

            if (!char.IsLetter(a[position]))
            {
                //if first character happens to be a number and not a letter

            }

            foreach (char c in a)
            {
                if (char.IsLetter(c))
                {
                    //change the first letter in a word to a capital letter, skipping any numbers or special characters
                    a[position] = char.ToUpper(a[position]);
                    //break once first letter has been capitalized
                    break;
                }
                //add one to position if characters happened to NOT be a letter
                position++;
            }
            return new string(a);
        }

        public void SetLanguageSetting()
        {
            foreach (Language l in Languages)
            {
                if (l.LevelOfListening.Equals(LanguageLevel.Basic) && l.LevelOfSpeaking.Equals(LanguageLevel.Basic) &&
                    l.LevelOfWriting.Equals(LanguageLevel.Basic))
                {
                    //probably using simple mode
                    l.IsSimple = !l.Level.Equals(LanguageLevel.Basic);
                }
                else
                {
                    l.IsSimple = false;
                }
            }
        }
    }
}