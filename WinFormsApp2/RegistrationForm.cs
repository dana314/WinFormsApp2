using System.Text.RegularExpressions;
using NLog;
namespace WinFormsApp2
{
    /// <summary>
    /// Форма регистрации нового пользователя в системе
    /// </summary>
    public partial class RegistrationForm : Form
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; }
        private User _currentUser;
        /// <summary>
        /// Заполняет поля формы данными пользователя
        /// </summary>
        public void SetUser(User user)
        {
            _currentUser = user;
            textBox1.Text = user.UserName;
            textBox2.Text = user.Email;
            textBox3.Text = user.Login;
            textBox4.Text = "";
            dateTimePicker1.Value = user.BirthDate;
        }
        /// <summary>
        /// Новая форму регистрации
        /// </summary>
        public RegistrationForm()
        {
            InitializeComponent();
            textBox4.PasswordChar = '*';
            dateTimePicker1.MaxDate = DateTime.Now;
        }
        /// <summary>
        /// Новая форма регистрации 
        /// </summary>
        public RegistrationForm(MainForm mainForm) : this()
        {
            _mainForm = mainForm;
        }

        private MainForm _mainForm;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private void registrationBTN_Click(object sender, EventArgs e)
        {
            Logger.Info("Начало регистрации");

            // Валидация имени пользователя
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                Logger.Error("Не введено имя пользователя");
                MessageBox.Show("Введите имя пользователя");
                textBox1.Focus();
                return;
            }

            // Валидация email
            var email = textBox2.Text.Trim();
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Logger.Error("Неверный формат email: {0}", email);
                MessageBox.Show("Введите корректный email");
                textBox2.Focus();
                return;
            }

            // Проверка даты рождения (не должна быть в будущем)
            if (dateTimePicker1.Value > DateTime.Now)
            {
                Logger.Error("Дата рождения позднее текущей даты");
                MessageBox.Show("Дата рождения не может быть задана в будущем");
                dateTimePicker1.Focus();
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    if (db.Users.Any(u => u.Login == textBox3.Text.Trim()))
                    {
                        MessageBox.Show("Этот телефон уже зарегистрирован");
                        return;
                    }

                    var newUser = new User
                    {
                        UserName = textBox1.Text.Trim(),
                        Email = textBox2.Text.Trim(),
                        Login = textBox3.Text.Trim(),
                        Password = BCrypt.Net.BCrypt.HashPassword(textBox4.Text),
                        BirthDate = dateTimePicker1.Value
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    MessageBox.Show("Вы успешно зарегистрировались!");

                    var anketaForm = new AnketaForm(newUser.Id);
                    anketaForm.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка при регистрации", ex);
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}");
            }
        }

        private void cancelBTN_Click(object sender, EventArgs e)
        {
            Logger.Info("Регистрация отменена");
            this.Close();
        }
    }
}