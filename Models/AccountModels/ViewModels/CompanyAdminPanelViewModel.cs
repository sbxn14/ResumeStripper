using ResumeStripper.Models.Enums;
using System.Collections.Generic;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class CompanyAdminPanelViewModel
    {
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public Company Company { get; set; }
        
        //with this model, the company admin should be able to add/remove users depending on their packages
    }
}