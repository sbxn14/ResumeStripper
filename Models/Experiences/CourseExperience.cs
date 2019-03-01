using System;

namespace ResumeStripper.Models.Experiences
{
    public class CourseExperience : Experience
    {
        public string Name { get; set; }
        public DateTime Year { get; set; }
        public bool Certificate { get; set; }

        public CourseExperience()
        {

        }

        public CourseExperience(int cvid, string name, DateTime year, bool certifcate, string organizationName, string locationOrganization)
        {
            //begin constructor super
            CVID = cvid;
            OrganizationName = organizationName;
            LocationOrganization = locationOrganization;
            //end constructor super
            Name = name;
            Year = year;
            Certificate = certifcate;
        }
    }
}