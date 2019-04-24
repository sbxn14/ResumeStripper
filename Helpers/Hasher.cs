using System;
using System.Security.Cryptography;

namespace ResumeStripper.Helpers
{
    public class Hasher
    {
        public string GenerateSalt()
        {
            //size of 64 characters, can be changed
            const int size = 64;

            var saltBytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string Encrypt(string word, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            //10,000 iterations are used for hashing, which is the current recommendation under NIST guidelines.
            var rfcBytes = new Rfc2898DeriveBytes(word, saltBytes, 10000);

            return Convert.ToBase64String(rfcBytes.GetBytes(256));
        }

        public bool VerifyPassword(string inputPassword, string storedPassword, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);

            //10,000 iterations are used for hashing, which is the current recommendation under NIST guidelines.
            var rfcBytes = new Rfc2898DeriveBytes(inputPassword, saltBytes, 10000);

            //returns true or false based on if the input password matches the stored password
            return Convert.ToBase64String(rfcBytes.GetBytes(256)) == storedPassword;
        }
    }
}