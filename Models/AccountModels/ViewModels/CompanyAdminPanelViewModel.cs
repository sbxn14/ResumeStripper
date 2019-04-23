using ResumeStripper.Models.Enums;
using System.Collections.Generic;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class CompanyAdminPanelViewModel
    {
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public Company Company { get; set; }

        public int UserCount { get; set; }
        public int TotalAllowedUsers { get; set; }

        public string UserReturnError { get; set; }
        
        //with this model, the company admin should be able to add/remove users depending on their packages
    }
}