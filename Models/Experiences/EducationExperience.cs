using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models.Experiences
{
    public class EducationExperience : Experience
    {
        public string Name { get; set; }
        public string LevelOfEducation { get; set; }
        public bool Diploma { get; set; }

        [Column("BeginDate")]
        public DateTime BeginDate { get; set; }

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        public EducationExperience()
        {
        }
    }
}