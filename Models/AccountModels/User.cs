using ResumeStripper.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models.AccountModels
{
    public class User
    {
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

        //for testing
        public User(int id, string emailaddress, string password, string salt, UserRole role, Company userCompany)
        {
            ID = id;
            Emailaddress = emailaddress;
            Password = password;
            Salt = salt;
            Role = role;
            UserCompany = userCompany;
        }
    }
}