using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models.Experiences
{
    public class CourseExperience : Experience
    {
        [Required(ErrorMessage = "Course name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Course name should be atleast 2 characters!")]
        public string Name { get; set; }

        [Column("Year")]
        [Required(ErrorMessage = "Year is required!")]
        public DateTime Year { get; set; }

        public bool Certificate { get; set; }

        public CourseExperience()
        {

        }

        //for testing
        public CourseExperience(string name, string location, DateTime year, string organizationname)
        {
            Name = name;
            OrganizationName = organizationname;
            LocationOrganization = location;
            Certificate = true;
            Year = year;
        }
    }
}