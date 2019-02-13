namespace ResumeStripper.Models.Experiences
{
    public class Experience
    {
        public int ID { get; set; }

        //CVID is the foreign key for ID of the CV
        public int CVID { get; set; }

        public string OrganizationName { get; set; }
        public string LocationOrganization { get; set; }
    }
}