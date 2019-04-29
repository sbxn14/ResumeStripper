using System;
using System.Linq;

namespace ResumeStripper.Helpers
{
    public static class RandomHelper
    {
        public static string RandomString(int length)
        {
            if (length == 0)
            {
                length = 1;
            }

            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}