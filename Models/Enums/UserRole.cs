using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models.Enums
{
    public enum UserRole
    {
        [Display(Name = "Company User")]
        CompanyUser = 1,
        [Display(Name = "Company Administrator")]
        CompanyAdmin = 2,
        [Display(Name = "EHV User")]
        EHVUser = 3,
        [Display(Name = "EHV Administrator")]
        EHVAdmin = 4
    }
}