using System;
using System.Collections.Generic;
using OneTimePasswordApp.Entities;
using OneTimePasswordApp.Interfaces;

namespace OneTimePasswordApp.Implementation
{
    public class PasswordManager : IPasswordManager
    {
        private readonly IDictionary<string, Password> passwordList = new Dictionary<string, Password>();

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

        public bool ValidatePassword(string id, string password)
        {
            if (!passwordList.ContainsKey(id))
                return false;
            var storedPassword = passwordList[id];
            var validation = storedPassword.Value == password && ((DateTime.UtcNow - storedPassword.GenerationTime).TotalMilliseconds < storedPassword.Timeout);
            if (validation)
            {
                passwordList.Remove(id);
            }
            return validation;
        }
    }
}