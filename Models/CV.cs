﻿using ResumeStripper.Models.Experiences;
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

        public void SetAnonymousCv()
        {
            //sets bool to true
            IsAnonymous = true;
            //TODO: eventueel veranderen van persoonlijke waardes naar "***" of iets dergelijks
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
                    char[] a = e.Name.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.Name = new string(a);

                    a = e.LevelOfEducation.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.LevelOfEducation = new string(a);

                    a = e.OrganizationName.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.OrganizationName = new string(a);

                    a = e.LocationOrganization.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.LocationOrganization = new string(a);
                }
            }

            if (WorkExperiences.Count > 0)
            {
                foreach (WorkExperience e in WorkExperiences)
                {
                    char[] a = e.JobTitle.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.JobTitle = new string(a);

                    a = e.TaskDescription.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.TaskDescription = new string(a);

                    a = e.OrganizationName.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.OrganizationName = new string(a);

                    a = e.LocationOrganization.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.LocationOrganization = new string(a);
                }
            }

            if (Courses.Count > 0)
            {
                foreach (CourseExperience e in Courses)
                {
                    char[] a = e.Name.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.Name = new string(a);

                    a = e.OrganizationName.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.OrganizationName = new string(a);

                    a = e.LocationOrganization.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.LocationOrganization = new string(a);
                }
            }

            if (SideLines.Count > 0)
            {
                foreach (SidelineExperience e in SideLines)
                {
                    char[] a = e.JobTitle.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.JobTitle = new string(a);

                    a = e.TaskDescription.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.TaskDescription = new string(a);

                    a = e.OrganizationName.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.OrganizationName = new string(a);

                    a = e.LocationOrganization.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    e.LocationOrganization = new string(a);
                }
            }
        }
    }
}