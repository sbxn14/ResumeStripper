using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeStripper.Models.Viewmodels
{
    public class MessageViewModel
    {
        public string Path { get; set; }
        public string Text { get; set; }
        public string serverPath { get; set; }
        public CV ResultCv { get; set; }
    }
}