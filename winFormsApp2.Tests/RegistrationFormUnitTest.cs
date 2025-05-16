using WinFormsApp2;
using System.Windows.Forms;

namespace winFormsApp2.Tests
{
    public class RegistrationFormUnitTest
    {
        [Fact]
        public void RegistrationBTN_Click_InvalidUsername_ShowsErrorMessage()
        {
            RegistrationForm form = new RegistrationForm();
            form.textBox1 = new TextBox();
            form.textBox2 = new TextBox();
            form.textBox3 = new TextBox();
            form.textBox4 = new TextBox();
            form.dateTimePicker1 = new DateTimePicker();

            form.textBox1.Text = "";

            bool messageBoxShown = false;
            string messageBoxText = "";

            form.MessageBoxShow = (text) =>
            {
                messageBoxShown = true;
                messageBoxText = text;
            };

            form.registrationBTN_Click(null, EventArgs.Empty);
            Assert.True(messageBoxShown, "Введите имя пользователя");
        }

        [Fact]
        public void CancelBTN_Click_ClosesForm()
        {
            var form = new RegistrationForm();

            form.cancelBTN_Click(null, EventArgs.Empty);

            Assert.True(form.IsDisposed);
        }

        [Fact]
        public void RegistrationBTN_Click_InvalidEmail_ShowsErrorMessage()
        {
            var form = new RegistrationForm();
            form.textBox1 = new TextBox();
            form.textBox2 = new TextBox();
            form.textBox3 = new TextBox();
            form.textBox4 = new TextBox();
            form.dateTimePicker1 = new DateTimePicker();

            bool messageBoxShown = false;
            string messageBoxText = "";

            form.MessageBoxShow = (text) =>
            {
                messageBoxShown = true;
                messageBoxText = text;
            };

            form.registrationBTN_Click(null, EventArgs.Empty);

            Assert.True(messageBoxShown, "Введите email");
        }

        [Fact]
        public void RegistrationBTN_Click_InvalidPhoneNumber_ShowsErrorMessage()
        {
            var form = new RegistrationForm();
            form.textBox1 = new TextBox();
            form.textBox2 = new TextBox();
            form.textBox3 = new TextBox();
            form.textBox4 = new TextBox();
            form.dateTimePicker1 = new DateTimePicker();

            form.textBox1.Text = "TestUser";
            form.textBox2.Text = "test@example.com";
            form.textBox3.Text = "123456789";

            bool messageBoxShown = false;
            string messageBoxText = "";

            form.MessageBoxShow = (text) =>
            {
                messageBoxShown = true;
                messageBoxText = text;
            };

            form.registrationBTN_Click(null, EventArgs.Empty);

            Assert.True(messageBoxShown, "Введите номер телефона в виде: +XXXXXXXXXXX");
        }

        [Fact]
        public void RegistrationBTN_Click_FutureBirthDate_ShowsErrorMessage()
        {
            var form = new RegistrationForm();
            form.textBox1 = new TextBox();
            form.textBox2 = new TextBox();
            form.textBox3 = new TextBox();
            form.textBox4 = new TextBox();
            form.dateTimePicker1 = new DateTimePicker();

            form.textBox1.Text = "TestUser";
            form.textBox2.Text = "test@example.com";
            form.textBox3.Text = "+79123456789";
            form.dateTimePicker1.Value = DateTime.Now.AddDays(1);

            bool messageBoxShown = false;
            string messageBoxText = "";

            form.MessageBoxShow = (text) =>
            {
                messageBoxShown = true;
                messageBoxText = text;
            };

            form.registrationBTN_Click(null, EventArgs.Empty);

            Assert.True(messageBoxShown, "Дата рождения не может быть задана в будущем");
        }
    }
}
