using ResumeStripper.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class EditCompanyViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a location!")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please enter a sector!")]
        public string Sector { get; set; }
        //package determines how much Users a company can have, or maybe how many CV's they can convert per month
        [Required(ErrorMessage = "Please select a package!")]
        public StripperPackage Package { get; set; }
        public List<User> Users { get; set; }
        public List<CV> Cvs { get; set; }

        public UserRole CurrentUserRole { get; set; }
    }
}