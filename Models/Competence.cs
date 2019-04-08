using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models
{
    public class Competence
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Competence name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Competence name should be atleast 2 characters!")]
        public string Name { get; set; }

        public Competence()
        {
        }

        public Competence(string name)
        {
            Name = name;
        }
    }
}