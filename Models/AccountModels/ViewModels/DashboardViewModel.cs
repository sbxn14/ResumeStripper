using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class DashboardViewModel
    {
        [EmailAddress]
        public string Emailaddress { get; set; }
        public Company Company { get; set; }
    }
}