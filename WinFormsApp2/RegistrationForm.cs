using System.Text.RegularExpressions;
namespace WinFormsApp2
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            textBox4.PasswordChar = '*';
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-9); // возраст - от 9 лет
        }
        private void registrationBTN_Click(object sender, EventArgs e)
        {
            // валидация введенных пользователем данных
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите имя пользователя");
                textBox1.Focus();
                return;
            }
            if (!textBox2.Text.Contains("@") || !textBox2.Text.Contains(".") || textBox2.Text.Length < 5)
            {
                MessageBox.Show("Введите email");
                textBox2.Focus();
                return;
            }
            if (!Regex.IsMatch(textBox3.Text, @"^\+[0-9]{11,20}$"))
            {
                MessageBox.Show("Введите номер телефона в виде: +XXXXXXXXXXX");
                textBox3.Focus();
                return;
            }
            //if (textBox4.Text.Length < 6)
            //{
            //    MessageBox.Show("Пароль должен быть 6-значным");
            //    textBox4.Focus();
            //    return;
            //}
            if (dateTimePicker1.Value > DateTime.Now.AddYears(-9))
            {
                MessageBox.Show("Минимальный возраст - 9 лет");
                dateTimePicker1.Focus();
                return;
            }
            try // проверка что данные пользователя есть в бд
            {
                var newUser = new User
                {
                    UserName = textBox1.Text.Trim(),
                    Email = textBox2.Text.Trim(),
                    Login = textBox3.Text.Trim(),
                    Password = BCrypt.Net.BCrypt.HashPassword(textBox4.Text),
                    BirthDate = dateTimePicker1.Value
                };
                using (var db = new AppDbContext())
                {
                    if (db.Users.Any(u => u.Login == newUser.Login))
                    {
                        MessageBox.Show("Этот телефон уже зарегистрирован");
                        return;
                    }
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    MessageBox.Show("Вы зарегистрировались!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}");
            }
        }
        private void cancelBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}