using NUnit.Framework;
using OneTimePasswordApp.Interfaces;

namespace OneTimePasswordApp.Tests
{
    [TestFixture]
    public class PasswordGenerator
    {
        [Test]
        public void Generate_ReturnsRandomlyGeneratedPassword()
        {
            IPasswordGenerator passwordGeneratorOne = new Implementation.PasswordGenerator();
            IPasswordGenerator passwordGeneratorTwo = new Implementation.PasswordGenerator();

            var passwordOne = passwordGeneratorOne.Generate();
            var passwordTwo = passwordGeneratorTwo.Generate();

            Assert.AreNotEqual(passwordOne, passwordTwo);
        }
    }
}
