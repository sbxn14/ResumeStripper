using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models
{
    public class Reference
    {
        public int ID { get; set; }

        //CVID is the foreign key for ID of the CV
        public int CVID { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be atleast 2 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Company name should be atleast 2 characters!")]
        [DisplayName("Company name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Job title is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Job title should be atleast 2 characters!")]
        [DisplayName("Job title")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Emailaddress is required!")]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phonenumber is required!")]
        [Phone]
        public string PhoneNumber { get; set; }

        public Reference()
        {
        }

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