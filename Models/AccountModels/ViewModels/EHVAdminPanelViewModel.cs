using ResumeStripper.Models.Enums;
using System.Collections.Generic;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class EHVAdminPanelViewModel
    {
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public List<User> Users { get; set; }
        public List<Company> Companies { get; set; }

        //With this model, admin should be able to add/remove Companies, their users, their admins and EHV users/admins
    }
}