using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ResumeStripper.Models.Enums;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class CompanyRegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a location!")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please enter a company sector!")]
        public string Sector { get; set; }
        //package determines how much Users a company can have, or maybe how many CV's they can convert per month
        [Required(ErrorMessage = "Please select a package!")]
        public StripperPackage Package { get; set; }
    }
}