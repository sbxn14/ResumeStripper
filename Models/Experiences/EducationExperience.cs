using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models.Experiences
{
    public class EducationExperience : Experience
    {
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be atleast 2 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Level of Education is required!")]
        [DisplayName("Level of Education")]
        public string LevelOfEducation { get; set; }

        public bool Diploma { get; set; }

        [Column("BeginDate")]
        [Required(ErrorMessage = "Begin Date is required!")]
        [DisplayName("Begin Date")]
        public DateTime BeginDate { get; set; }

        [Column("EndDate")]
        [Required(ErrorMessage = "End date is required!")]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        public EducationExperience()
        {
        }
    }
}