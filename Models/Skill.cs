namespace ResumeStripper.Models
{
    public class Skill
    {
        public int ID { get; set; }
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