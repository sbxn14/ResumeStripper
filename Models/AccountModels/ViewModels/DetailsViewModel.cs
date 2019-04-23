using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResumeStripper.Models.Enums;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class DetailsViewModel
    {
        public string Emailaddress { get; set; }
        public UserRole Role { get; set; }
        public Company Company { get; set; }
        public UserRole CurrentUserRole { get; set; }
    }
}