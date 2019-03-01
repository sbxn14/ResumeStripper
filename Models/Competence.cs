using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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