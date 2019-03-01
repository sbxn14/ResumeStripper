using System;

namespace ResumeStripper.Models.Experiences
{
    public class WorkExperience : Experience
    {
        public string JobTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public WorkExperience()
        {

        }

        public WorkExperience(int cvid, string jobTitle, string taskDescription, string organizationName, string locationOrganization, DateTime beginDate, DateTime endDate)
        {
            //begin constructor super
            CVID = cvid;
            OrganizationName = organizationName;
            LocationOrganization = locationOrganization;
            //end constructor super
            JobTitle = jobTitle;
            TaskDescription = taskDescription;
            BeginDate = beginDate;
            EndDate = endDate;
        }
    }
}