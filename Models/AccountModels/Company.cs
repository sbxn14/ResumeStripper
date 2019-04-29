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

        [NotMapped]
        public List<User> Users { get; set; }

        public virtual List<CV> Cvs { get; set; }

        public Company()
        {

        }

        public Company(int id, string name, string location, string sector, StripperPackage package)
        {
            //for testing
            Id = id;
            Name = name;
            Location = location;
            Sector = sector;
            Package = package;
        }

        public int GetPackageUserCount()
        {
            int returnValue = 0;

            switch (Package)
            {
                case StripperPackage.A:
                    returnValue = 5;
                    break;
                case StripperPackage.B:
                    returnValue = 10;
                    break;
                case StripperPackage.C:
                    returnValue = 100;
                    break;
                case StripperPackage.EHV:
                    //will be set to infinite anyway, temporary
                    returnValue = 999;
                    break;
            }

            return returnValue;
        }

        //TODO: something with company colours and logo since they are recurring. also incorporate in generating CV and every view
    }
}