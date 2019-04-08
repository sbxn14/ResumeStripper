using ResumeStripper.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models
{
    public class License
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please select a licensetype or remove this field!")]
        public DriversLicense Type { get; set; }
    }
}