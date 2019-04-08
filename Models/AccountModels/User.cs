using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeStripper.Models.AccountModels
{
    [NotMapped]
    public class User
    {
        //TODO Proper encryption instead of salted hash
        [Key]
        public int ID { get; set; }

        public string Emailaddress { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        //for Role: 0 = EHV admin, 1 = Company Admin, 2 = User of Company
        public int Role { get; set; }

        public User()
        {
        }
    }
}