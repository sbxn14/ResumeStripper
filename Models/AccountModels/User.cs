using ResumeStripper.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models.AccountModels
{
    public class User
    {
        //TODO Proper encryption instead of salted hash
        [Key]
        public int ID { get; set; }
        public string Emailaddress { get; set; }
        public string Password { get; set; }
        //salt is saved as Base64String
        public string Salt { get; set; }
        public virtual UserRole Role { get; set; }
        public virtual Company UserCompany { get; set; }

        public User()
        {
        }
    }
}