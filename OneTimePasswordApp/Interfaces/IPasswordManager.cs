namespace OneTimePasswordApp.Interfaces
{
    public interface IPasswordManager
    {
        string CreatePassword(string userId, int timeout = 30000);
        bool ValidatePassword(string id, string password);
    }
}
