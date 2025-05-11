using WinFormsApp2;
using System.Windows.Forms;

namespace winFormsApp2.Tests
{
    public class LoginFormTests
    {
        [Fact]
        public void LoginBTN_Click_EmptyLogin_ShowsErrorMessage()
        {
            // Arrange
            LoginForm form = new LoginForm();
            form.textBox1 = new TextBox();
            form.textBox2 = new TextBox();
            form.textBox1.Text = "";
            form.textBox2.Text = "password";

            bool messageBoxShown = false;
            string messageBoxText = "";

            form.MessageBoxShow = (text) =>
            {
                messageBoxShown = true;
                messageBoxText = text;
            };

            // Act
            form.loginBTN_Click(null, EventArgs.Empty);

            // Assert
            Assert.True(messageBoxShown, "Введите номер и пароль");
        }

    }
}
