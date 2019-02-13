namespace ResumeStripper.Models
{
    public class Reference
    {
        public int ID { get; set; }

        //CVID is the foreign key for ID of the CV
        public int CVID { get; set; }

        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Reference(string name, string companyName, string jobTitle, string email, string phoneNumber)
        {
            Name = name;
            CompanyName = companyName;
            JobTitle = jobTitle;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}