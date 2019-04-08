using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models
{
    public class Skill
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Skill name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Skill name should be atleast 2 characters!")]
        public string Name { get; set; }

        public Skill()
        {
        }

        public Skill(string name)
        {
            Name = name;
        }
    }
}