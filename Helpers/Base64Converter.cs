using System;
using System.IO;

namespace ResumeStripper.Helpers
{
    public class Base64Converter
    {
        public string ImageToBase64(string path)
        {
            string complete = Path.GetFullPath(path);
            byte[] bytes = File.ReadAllBytes(complete);
            return Convert.ToBase64String(bytes);
        }
    }
}