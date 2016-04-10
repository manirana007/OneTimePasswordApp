using System;
using OneTimePasswordApp.Implementation;
using OneTimePasswordApp.Interfaces;

namespace OneTimePasswordApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IPasswordManager passwordManager = new PasswordManager(new PasswordGenerator());

            while (true)
            {
                Console.WriteLine("Please make a choice:");
                Console.WriteLine("Enter 1 to generate password");
                Console.WriteLine("Enter 2 to log in");
                Console.WriteLine("Enter 3 to exit");
                Console.Write("Option: ");
                var option = Console.ReadLine();
                if (string.IsNullOrEmpty(option))
                    option = "3";
                if (IsNumber(option))
                {
                    string id;
                    string password;
                    
                    var number = int.Parse(option);
                    switch (number)
                    {
                        case 1:
                            Console.Write("Please enter a user id: ");
                            id = Console.ReadLine();

                            password = passwordManager.CreatePassword(id);

                            Console.WriteLine("Generated password for {0} is: {1}", id, password);
                            break;
                        case 2:
                            Console.Write("Please enter user id:");
                            id = Console.ReadLine();

                            Console.Write("Please enter password:");
                            password = Console.ReadLine();

                            Console.WriteLine("Password valid: {0}", passwordManager.IsPasswordCorrectAndValid(id, password));
                            break;
                        case 3:
                            return;
                        default:
                            Console.WriteLine("Invalid option.  Please try again.");
                            break;
                    }
                }
            }
        }

        private static bool IsNumber(string text)
        {
            int n;
            return int.TryParse(text, out n);
        }
    }
}