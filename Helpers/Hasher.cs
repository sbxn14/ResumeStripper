using ResumeStripper.DAL;
using System;
using System.Security.Cryptography;

namespace ResumeStripper.Helpers
{
    public class Hasher
    {
        private StripperContext context;

        public Hasher(StripperContext cont)
        {
            context = cont;
        }

        public byte[] GenerateSalt()
        {
            //size of 15 characters, can be changed
            int size = 15;

            //generates a random (cryptographically sound) salt and returns it
            char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            return data;
        }


        public string Encrypt(string word)
        {
            //size of hash, can be changed
            int size = 20;
            //generate salt
            byte[] salt = GenerateSalt();
            //has and salt with PBKDF2 for 10000 iterations (can be changed to not last as long)
            var pbkdf2 = new Rfc2898DeriveBytes(word, salt, 10000);

            byte[] hash = pbkdf2.GetBytes(size);

            //size of 35 cause hash of 20 + salt of 15
            byte[] hashBytes = new byte[35];
            //place hash and salt in their places
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //TODO: save Hash and salt with the right user

            return Convert.ToBase64String(hashBytes);
        }
    }
}