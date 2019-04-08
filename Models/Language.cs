using ResumeStripper.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models
{
    public class Language
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Language name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Language name should be atleast 2 characters!")]
        public string Name { get; set; }

        //Level is used if CV doesn't separate listening, speaking and writing
        public virtual LanguageLevel Level { get; set; }

        public virtual LanguageLevel LevelOfListening { get; set; }
        public virtual LanguageLevel LevelOfSpeaking { get; set; }
        public virtual LanguageLevel LevelOfWriting { get; set; }
        public bool IsSimple { get; set; }

        public Language()
        {
            IsSimple = true;
        }
    }
}