using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models.Experiences
{
    public class CourseExperience : Experience
    {
        public string Name { get; set; }
        [Column("Year")]
        public DateTime Year { get; set; }
        public bool Certificate { get; set; }

        public CourseExperience()
        {

        }
    }
}