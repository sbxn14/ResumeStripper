using ResumeStripper.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models.AccountModels
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Sector { get; set; }
        //package determines how much Users a company can have, or maybe how many CV's they can convert per month
        public virtual StripperPackage Package { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<CV> Cvs { get; set; }

        public Company()
        {

        }

        public Company(string name, string location, string sector, StripperPackage package)
        {
            Name = name;
            Location = location;
            Sector = sector;
            Package = package;
        }

        //TODO: something with company colours and logo since they are recurring. also incorporate in generating CV and every view
    }
}