using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResumeStripper.Models.Enums;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string Emailaddress { get; set; }
        [Required(ErrorMessage = "Please select a role!")]
        public UserRole Role { get; set; }
        public Company UserCompany { get; set; }

        public List<Company> Companies { get; set; }

        public UserRole CurrentUserRole { get; set; }
    }
}