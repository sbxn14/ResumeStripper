using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models
{
    public class Hobby
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Hobby name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Hobby name should be atleast 2 characters!")]
        public string Name { get; set; }

        public Hobby()
        {
        }

        public Hobby(string name)
        {
            Name = name;
        }
    }
}