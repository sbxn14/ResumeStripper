using ResumeStripper.Models;
using System;
using System.Data.Entity;

namespace ResumeStripper.DAL
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<StripperContext>
    {
        protected override void Seed(StripperContext context)
        {
            ////base.Seed(context);

            ////make test CV
            //CV cv = new CV
            //{
            //    Name = "piet",
            //    Prefix = "van",
            //    Surname = "puk",
            //    Residence = "Eindhoven",
            //    Country = "The Netherlands",
            //    DateOfBirth = new DateTime(1997, 02, 05)
            //};
            //Console.WriteLine("you created a CV");
            ////var newExperience = new EducationExperience(1, "lol", "HBO", true, "lollol", "daar", DateTime.Now, DateTime.Now);
            ////cv.Educations.Add(newExperience);

            //context.CVS.Add(cv);
            //context.SaveChanges();
            //Console.WriteLine("you added a CV to the db");
        }
    }
}