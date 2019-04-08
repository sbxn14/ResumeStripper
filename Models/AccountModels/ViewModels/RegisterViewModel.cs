using ResumeStripper.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter an emailaddress!")]
        [EmailAddress]
        public string Email { get; set; }
        //checks if password matches the Regex requirements
        [Required(ErrorMessage = "Please enter a password!")]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Please enter a password that has atleast 8 characters, 1 uppercase letter, 1 lowercase letter and 1 digit or 1 special character!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter a confirmation password!")]
        [Compare("Password", ErrorMessage = "Your passwords don't match! Please try again!")]
        public string ConfirmPassword { get; set; }

        public Company Company { get; set; }
        public UserRoles Role { get; set; }
    }
}