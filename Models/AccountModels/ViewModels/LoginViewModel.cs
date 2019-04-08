﻿using System.ComponentModel.DataAnnotations;

namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool IsRemember { get; set; }
    }
}