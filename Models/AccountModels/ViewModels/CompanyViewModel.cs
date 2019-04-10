using ResumeStripper.Models.Enums;
using System.Collections.Generic;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class CompanyViewModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Sector { get; set; }
        //package determines how much Users a company can have, or maybe how many CV's they can convert per month
        public StripperPackage Package { get; set; }
        public List<User> Users { get; set; }
        public List<CV> Cvs { get; set; }
    }
}