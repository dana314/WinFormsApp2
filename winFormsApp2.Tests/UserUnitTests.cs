using WinFormsApp2;
using System.ComponentModel.DataAnnotations;

namespace winFormsApp2.Tests
{
    public class UserUnitTests
    {
        private static IList<ValidationResult> ValidateModel(object model)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
        [Fact]
        public void User_ValidData_IsValid()
        {
            User user = new User
            {
                Login = "79123456789",
                UserName = "TestUser",
                Email = "test@example.com",
                Password = "StrongPassword",
                BirthDate = new DateTime(1990, 1, 1)
            };

            IList<ValidationResult> validationResults = ValidateModel(user);

            Assert.NotEmpty(validationResults);
        }

        [Fact]
        public void User_InvalidLogin_IsInvalid()
        {
            User user = new User
            {
                Login = "invalid-phone",
                UserName = "TestUser",
                Email = "test@example.com",
                Password = "StrongPassword",
                BirthDate = new DateTime(1990, 1, 1)
            };

            IList<ValidationResult> validationResults = ValidateModel(user);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Login") && vr.ErrorMessage.Contains("Введите номер телефона"));
        }

        [Fact]
        public void User_MissingUserName_IsInvalid()
        {
            User user = new User
            {
                Login = "+79123456789",
                UserName = null,
                Email = "test@example.com",
                Password = "StrongPassword",
                BirthDate = new DateTime(1990, 1, 1)
            };

            IList<ValidationResult> validationResults = ValidateModel(user);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("UserName") && vr.ErrorMessage.Contains("Имя пользователя обязательно"));
        }

        [Fact]
        public void User_InvalidEmail_IsInvalid()
        {
            User user = new User
            {
                Login = "+79123456789",
                UserName = "TestUser",
                Email = null,
                Password = "StrongPassword",
                BirthDate = new DateTime(1990, 1, 1)
            };

            IList<ValidationResult> validationResults = ValidateModel(user);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Email") && vr.ErrorMessage.Contains("Почта обязательна"));
        }

        [Fact]
        public void User_MissingPassword_IsInvalid()
        {
            User user = new User
            {
                Login = "+79123456789",
                UserName = "TestUser",
                Email = "test@example.com",
                Password = null,
                BirthDate = new DateTime(1990, 1, 1)
            };

            IList<ValidationResult> validationResults = ValidateModel(user);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Password") && vr.ErrorMessage.Contains("Пароль обязателен"));
        }
    }
}
