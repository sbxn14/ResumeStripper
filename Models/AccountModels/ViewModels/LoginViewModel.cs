﻿namespace ResumeStripper.Models.AccountModels.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool isRemember { get; set; }
    }
}