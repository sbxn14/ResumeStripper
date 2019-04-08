using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResumeStripper.Models.Enums
{
    public enum UserRoles
    {
        [Display(Name = "Company User")]
        CompanyUser,
        [Display(Name = "Company Administrator")]
        CompanyAdmin,
        [Display(Name = "EHV User")]
        EHVUser,
        [Display(Name = "EHV Administrator")]
        EHVAdmin
    }
}