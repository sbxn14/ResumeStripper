using ResumeStripper.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter an emailaddress!")]
        [EmailAddress(ErrorMessage = "Please enter an valid emailaddress!")]
        public string Email { get; set; }
        //checks if password matches the Regex requirements
        [Required(ErrorMessage = "Please enter a password!")]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Please enter a password that has atleast 8 characters, 1 uppercase letter, 1 lowercase letter and 1 digit or 1 special character!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter a confirmation password!")]
        [Compare("Password", ErrorMessage = "Your passwords don't match! Please try again!")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please select a company!")]
        public Company Company { get; set; }
        [Required(ErrorMessage = "Please select a role!")]
        public UserRole Role { get; set; }

        public List<Company> Companies { get; set; }

        public UserRole CurrentUserRole { get; set; }
        public string CurrentCompanyName { get; set; }
    }
}