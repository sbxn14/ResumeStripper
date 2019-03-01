using System;

namespace ResumeStripper.Models.Experiences
{
    public class EducationExperience : Experience
    {
        public string Name { get; set; }
        public string LevelOfEducation { get; set; }
        public bool Diploma { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public EducationExperience()
        {

        }

        public EducationExperience(int cvid, string name, string levelofEducation, bool diploma, string organizationName, string locationOrganization, DateTime beginDate, DateTime endDate)
        {
            //begin constructor super
            CVID = cvid;
            OrganizationName = organizationName;
            LocationOrganization = locationOrganization;
            //end constructor super
            Name = name;
            LevelOfEducation = levelofEducation;
            Diploma = diploma;
            BeginDate = beginDate;
            EndDate = endDate;
        }
    }
}