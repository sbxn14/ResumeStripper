using ResumeStripper.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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

        public User()
        {

        }
    }
}