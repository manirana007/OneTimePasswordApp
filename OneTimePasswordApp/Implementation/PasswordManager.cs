using System;
using System.Collections.Generic;
using OneTimePasswordApp.Entities;
using OneTimePasswordApp.Interfaces;

namespace OneTimePasswordApp.Implementation
{
    public class PasswordManager : IPasswordManager
    {
        private IDictionary<string, Password> passwordList = new Dictionary<string, Password>();

        private readonly IPasswordGenerator passwordGenerator;

        public PasswordManager(IPasswordGenerator passwordGenerator)
        {
            this.passwordGenerator = passwordGenerator;
        }

        public string CreatePassword(string userId, int timeout)
        {
            var password = this.passwordGenerator.Generate();
            if (passwordList.ContainsKey(userId))
            {
                passwordList.Remove(userId);
            }
            passwordList.Add(new KeyValuePair<string, Password>(userId, new Password(password, timeout)));
            return password;
        }

        public bool IsPasswordCorrectAndValid(string userId, string password)
        {
            if (!passwordList.ContainsKey(userId))
            {
                return false;
            }
            var storedPassword = passwordList[userId];
            var isValid = storedPassword.Value == password && PasswordHasNotExpired(storedPassword);
            if (isValid)
            {
                passwordList.Remove(userId);
            }
            return isValid;
        }

        private static bool PasswordHasNotExpired(Password storedPassword)
        {
            return (Math.Abs((DateTime.UtcNow - storedPassword.GenerationTime).TotalMilliseconds) < storedPassword.TimeoutMilliseconds);
        }
    }
}