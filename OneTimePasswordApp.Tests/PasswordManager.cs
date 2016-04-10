using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using OneTimePasswordApp.Interfaces;

namespace OneTimePasswordApp.Tests
{
    [TestFixture]
    public class PasswordManager
    {
        [Test]
        public void CreatePassword_ShouldReturnsPasswordForGivenUserId()
        {
            //Arrange
            Mock<IPasswordGenerator> passwordGenerator = new Mock<IPasswordGenerator>(MockBehavior.Strict);
            passwordGenerator.Setup(x => x.Generate()).Returns("password");

            //Act
            IPasswordManager passwordManager = new Implementation.PasswordManager(passwordGenerator.Object);
            var password = passwordManager.CreatePassword("id");
            
            //Assert
            Assert.AreEqual("password", password);
        }

        [Test]
        public void CreatePassword_GivenSameUserIdMultipleTimes()
        {
            //Arrange
            IPasswordGenerator passwordGenerator = new Implementation.PasswordGenerator();

            //Act
            IPasswordManager passwordManager = new Implementation.PasswordManager(passwordGenerator);
            var passwordOne = passwordManager.CreatePassword("id");
            var passwordTwo = passwordManager.CreatePassword("id");
            var passwordValidated = passwordManager.ValidatePassword("id", passwordOne);

            //Assert
            Assert.IsFalse(passwordValidated);
            passwordValidated = passwordManager.ValidatePassword("id", passwordTwo);
            Assert.IsTrue(passwordValidated);
        }
        
        [Test]
        public void ValidatePassword_GivenCorrectPasswordForUserIdWithinTimeLimitShouldReturnValidatePassword()
        {
            //Arrange
            Mock<IPasswordGenerator> passwordGenerator = new Mock<IPasswordGenerator>(MockBehavior.Strict);
            passwordGenerator.Setup(x => x.Generate()).Returns("password");

            //Act
            IPasswordManager passwordManager = new Implementation.PasswordManager(passwordGenerator.Object);
            var password = passwordManager.CreatePassword("id");
            var passwordValidated = passwordManager.ValidatePassword("id", password);

            //Assert
            Assert.IsTrue(passwordValidated);
        }
        
        [Test]
        public void ValidatePassword_GivenIncorrectPasswordForUserIdWithinTimeLimitShouldInvalidatePassword()
        {
            //Arrange
            Mock<IPasswordGenerator> passwordGenerator = new Mock<IPasswordGenerator>(MockBehavior.Strict);
            passwordGenerator.Setup(x => x.Generate()).Returns("password");

            //Act
            IPasswordManager passwordManager = new Implementation.PasswordManager(passwordGenerator.Object);
            passwordManager.CreatePassword("id");
            var passwordValidated = passwordManager.ValidatePassword("id", "wrong password");

            //Assert
            Assert.IsFalse(passwordValidated);
        }
        
        [Test]
        public async Task ValidatePassword_GivenCorrectPasswordForUserIdOutsideTimeLimitShouldInvalidatePassword()
        {
            //Arrange
            Mock<IPasswordGenerator> passwordGenerator = new Mock<IPasswordGenerator>(MockBehavior.Strict);
            passwordGenerator.Setup(x => x.Generate()).Returns("password");
            
            //Act
            IPasswordManager passwordManager = new Implementation.PasswordManager(passwordGenerator.Object);
            var password = passwordManager.CreatePassword("id", 1);
            await Task.Delay(1);
            var passwordValidated = passwordManager.ValidatePassword("id", password);

            //Assert
            Assert.IsFalse(passwordValidated);
        }
        
        [Test]
        public async Task ValidatePassword_GivenCorrectPasswordForUserIdOutsideDefaultTimeLimitShouldInvalidePassword()
        {
            //Arrange
            Mock<IPasswordGenerator> passwordGenerator = new Mock<IPasswordGenerator>(MockBehavior.Strict);
            passwordGenerator.Setup(x => x.Generate()).Returns("password");

            //Act
            IPasswordManager passwordManager = new Implementation.PasswordManager(passwordGenerator.Object);
            var password = passwordManager.CreatePassword("id");
            await Task.Delay(30000);
            var passwordValidated = passwordManager.ValidatePassword("id", password);

            //Assert
            Assert.IsFalse(passwordValidated);
        }
        
        [Test]
        public void ValidatePassword_GivenInvalidUserIdAndPassword()
        {
            //Arrange
            Mock<IPasswordGenerator> passwordGenerator = new Mock<IPasswordGenerator>(MockBehavior.Strict);
            passwordGenerator.Setup(x => x.Generate()).Returns("password");

            //Act
            IPasswordManager passwordManager = new Implementation.PasswordManager(passwordGenerator.Object);
            var password = passwordManager.CreatePassword("id");
            var passwordValidated = passwordManager.ValidatePassword("invalid id", password);

            //Assert
            Assert.IsFalse(passwordValidated);
        }

        
        
        [Test]
        public void ValidatePassword_ShouldAllowOneTimeAccessWithPassword()
        {
            //Arrange
            IPasswordGenerator passwordGenerator = new Implementation.PasswordGenerator();

            //Act
            IPasswordManager passwordManager = new Implementation.PasswordManager(passwordGenerator);
            var password = passwordManager.CreatePassword("id");
            var passwordValidated = passwordManager.ValidatePassword("id", password);

            //Assert
            Assert.IsTrue(passwordValidated);
            passwordValidated = passwordManager.ValidatePassword("id", password);
            Assert.IsFalse(passwordValidated);
        }
    }
}
