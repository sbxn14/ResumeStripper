using ResumeStripper.Models.Enums;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class UserViewModel
    {
        public string Emailaddress { get; set; }
        public UserRole Role { get; set; }
        public Company UserCompany { get; set; }
    }
}