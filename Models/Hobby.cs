using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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