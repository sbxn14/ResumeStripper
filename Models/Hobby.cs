using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models
{
    public class Hobby
    {
        [Key]
        public int ID { get; set; }

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