using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ResumeStripper.Models.Enums;

namespace ResumeStripper.Models.AccountModels
{
    [NotMapped]
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Sector { get; set; }
        public StripperPackage Package { get; set; }
        public List<User> Users { get; set; }
        public List<CV> Cvs { get; set; }

        //TODO: something with company colours and logo since they are recurring. also incorporate in generating CV and every view
    }
}