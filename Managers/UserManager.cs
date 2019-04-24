using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResumeStripper.DAL;
using ResumeStripper.Helpers;
using ResumeStripper.Models.AccountModels;
using ResumeStripper.Models.AccountModels.ViewModels;

namespace ResumeStripper.Managers
{
    public class UserManager
    {
        private UserRepository Repo;
        private Hasher hasher;

        public UserManager(UserRepository repo)
        {
            this.Repo = repo;
            hasher = new Hasher();
        }

        public bool CheckIfUserExists(string email)
        {
            var result = Repo.GetUserByEmail(email);
            //returns true if user exists in the database
            return result != null;
        }

        public User FillUser(RegisterViewModel model)
        {
            var salt = MakeSalt();
            var password = PerformHash(model.Password, salt);

            User u = new User
            {
                Emailaddress = model.Email,
                Password = password,
                Salt = salt,
                Role = model.Role,
                UserCompany = model.Company
            };

            return u;
        }

        private string MakeSalt()
        {
            //generates salt
            return hasher.GenerateSalt();
        }

        private string PerformHash(string password, string salt)
        {
            //hashes password
            return hasher.Encrypt(password, salt);
        }
    }
}