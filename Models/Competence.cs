using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models
{
    public class Competence
    {
        [Key]
        public int ID { get; set; }

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