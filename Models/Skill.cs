using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models
{
    public class Skill
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Skill name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Skill name should be atleast 2 characters!")]
        public string Name { get; set; }
        //CVID is the foreign key for ID of the CV
        [ForeignKey("Cv")]
        public int CVID { get; set; }
        public virtual CV Cv { get; set; }
        public Skill()
        {
        }

        public Skill(string name)
        {
            //for testing
            Name = name;
        }
    }
}