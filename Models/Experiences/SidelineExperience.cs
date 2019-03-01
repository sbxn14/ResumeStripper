using System;

namespace ResumeStripper.Models.Experiences
{
    public class SidelineExperience : Experience
    {
        public string JobTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public SidelineExperience()
        {

        }

        public SidelineExperience(int cvid, string jobTitle, string taskDescription, string organizationName, string locationOrganization, DateTime beginDate, DateTime endDate)
        {
            //begin constructor (experience)super
            CVID = cvid;
            OrganizationName = organizationName;
            LocationOrganization = locationOrganization;
            //end constructor (experience)super
            JobTitle = jobTitle;
            TaskDescription = taskDescription;
            BeginDate = beginDate;
            EndDate = endDate;
        }
    }
}