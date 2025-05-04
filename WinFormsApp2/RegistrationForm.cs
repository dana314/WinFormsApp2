using System.Text.RegularExpressions;
namespace WinFormsApp2
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            textBox4.PasswordChar = '*';
            dateTimePicker1.MaxDate = DateTime.Now; 
        }
        public void registrationBTN_Click(object sender, EventArgs e)
        {
            Logger.Log("Начало регистрации");
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                Logger.Log("Не введено имя пользователя");
                MessageBox.Show("Введите имя пользователя");
                textBox1.Focus();
                return;
            }

            if (!textBox2.Text.Contains("@") || !textBox2.Text.Contains(".") || textBox2.Text.Length < 5)
            {
                Logger.Log("Неверный формат email");
                MessageBox.Show("Введите email");
                textBox2.Focus();
                return;
            }

            if (!Regex.IsMatch(textBox3.Text, @"^\+[0-9]{11,20}$"))
            {
                Logger.Log("Неверный формат номера телефона");
                MessageBox.Show("Введите номер телефона в виде: +XXXXXXXXXXX");
                textBox3.Focus();
                return;
            }

            if (dateTimePicker1.Value > DateTime.Now)
            {
                Logger.Log("Дата рождения позднее текущей даты");
                MessageBox.Show("Дата рождения не может быть задана в будущем");
                dateTimePicker1.Focus();
                return;
            }
            try
            {
                var newUser = new User
                {
                    UserName = textBox1.Text.Trim(),
                    Email = textBox2.Text.Trim(),
                    Login = textBox3.Text.Trim(),
                    Password = BCrypt.Net.BCrypt.HashPassword(textBox4.Text),
                    BirthDate = dateTimePicker1.Value
                };

                Logger.Log($"Новый пользователь: {newUser.UserName}");

                using (var db = new AppDbContext())
                {
                    if (db.Users.Any(u => u.Login == newUser.Login))
                    {
                        Logger.Log($"Ошибка: Телефон {newUser.Login} уже зарегистрирован");
                        MessageBox.Show("Этот телефон уже зарегистрирован");
                        return;
                    }

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    Logger.Log($"Регистрация пользователя: {newUser.UserName}");
                    MessageBox.Show("Вы зарегистрировались!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка при регистрации", ex);
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}");
            }
            AnketaForm anketa = new AnketaForm();
            anketa.Show();
        }
        public void cancelBTN_Click(object sender, EventArgs e)
        {
            Logger.Log("Регистрация отменена");
            this.Close();
        }
        public void textBox1_TextChanged(object sender, EventArgs e) { }
        public void textBox2_TextChanged(object sender, EventArgs e) { }
        public void textBox3_TextChanged(object sender, EventArgs e) { }
        public void textBox4_TextChanged(object sender, EventArgs e) { }
        public void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
    }
}