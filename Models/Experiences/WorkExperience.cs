using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models.Experiences
{
    public class WorkExperience : Experience
    {
        [Required(ErrorMessage = "Job title is required!")]
        [DisplayName("Job Title")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Job title should be atleast 2 characters!")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Course name is required!")]
        [DisplayName("Task Description")]
        [StringLength(2000, MinimumLength = 2, ErrorMessage = "Course name should be atleast 2 and less than 2000 characters!")]
        public string TaskDescription { get; set; }

        [Column("BeginDate")]
        [Required(ErrorMessage = "Begin Date is required!")]
        [DisplayName("Begin Date")]
        public DateTime BeginDate { get; set; }

        [Column("EndDate")]
        [Required(ErrorMessage = "End date is required!")]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        public WorkExperience()
        {
        }
    }
}