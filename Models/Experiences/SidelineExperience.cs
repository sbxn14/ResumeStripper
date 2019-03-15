using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models.Experiences
{
    public class SidelineExperience : Experience
    {
        public string JobTitle { get; set; }
        public string TaskDescription { get; set; }

        [Column("BeginDate")]
        public DateTime BeginDate { get; set; }

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        public SidelineExperience()
        {
        }
    }
}