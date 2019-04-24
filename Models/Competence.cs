using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models
{
    public class Competence
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Competence name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Competence name should be atleast 2 characters!")]
        public string Name { get; set; }
        //CVID is the foreign key for ID of the CV
        [ForeignKey("Cv")]
        public int CvID { get; set; }
        public virtual CV Cv { get; set; }

        public Competence()
        {
        }

        public Competence(string name)
        {
            //for testing
            Name = name;
        }
    }
}