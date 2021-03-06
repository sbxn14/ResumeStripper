﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models.Experiences
{
    public class Experience
    {
        public int ID { get; set; }

        //CVID is the foreign key for ID of the CV
        [ForeignKey("Cv")]
        public int CVID { get; set; }
        public virtual CV Cv { get; set; }

        [Required(ErrorMessage = "Organization Name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Organization Name should be atleast 2 characters!")]
        [DisplayName("Organization Name")]
        public string OrganizationName { get; set; }

        [Required(ErrorMessage = "Organization Location is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Organization location should be atleast 2 characters!")]
        [DisplayName("Organization Location")]
        public string LocationOrganization { get; set; }
    }
}