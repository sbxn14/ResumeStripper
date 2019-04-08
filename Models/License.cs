using ResumeStripper.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models
{
    public class License
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please select a licensetype or remove this field!")]
        public DriversLicense Type { get; set; }
        public List<int> CVIDs { get; set; }
        public virtual List<CV> CVs { get; set; }
    }
}