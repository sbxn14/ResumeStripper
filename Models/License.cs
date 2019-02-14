using ResumeStripper.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeStripper.Models
{
    public class License
    {
        public int ID { get; set; }
        public DriversLicense Type { get; set; }
    }
}