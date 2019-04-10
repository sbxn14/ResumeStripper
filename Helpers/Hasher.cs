using System;
using System.Security.Cryptography;

namespace ResumeStripper.Helpers
{
    public class Hasher
    {
        public string GenerateSalt()
        {
            //size of 15 characters, can be changed
            const int size = 64;

            var saltBytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);

            ////generates a random (cryptographically sound) salt and returns it
            //char[] chars =
            //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            //byte[] data = new byte[size];
            //using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            //{
            //    crypto.GetBytes(data);
            //}
            //return data;
        }

        public string Encrypt(string word, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            //10,000 iterations are used for hashing, which is the current recommendation under NIST guidelines.
            var rfcBytes = new Rfc2898DeriveBytes(word, saltBytes, 10000);

            return Convert.ToBase64String(rfcBytes.GetBytes(256));

            ////size of hash, can be changed
            //const int size = 20;
            ////has and salt with PBKDF2 for 10000 iterations (can be changed to not last as long)
            //var pbkdf2 = new Rfc2898DeriveBytes(word, salt, 10000);

            //byte[] hash = pbkdf2.GetBytes(size);

            ////size of 35 cause hash of 20 + salt of 15
            //byte[] hashBytes = new byte[35];
            ////place hash and salt in their places
            //Array.
            //Array.Copy(salt, 0, hashBytes, 0, 16);
            //Array.Copy(hash, 0, hashBytes, 16, 20);

            ////TODO: save Hash and salt with the right user

            //return Convert.ToBase64String(hashBytes);
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