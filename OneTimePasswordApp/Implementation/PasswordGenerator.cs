using System;
using OneTimePasswordApp.Interfaces;

namespace OneTimePasswordApp.Implementation
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string Generate()
        {
            var passwordLength = 10;
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];

            var seed = (int)DateTime.Now.Ticks; 
            var random = new Random(seed);

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
