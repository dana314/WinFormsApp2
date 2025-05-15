using NLog;
namespace WinFormsApp2
{
    /// <summary>
    /// Форма авторизации пользователя в системе
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// ID текущего авторизованного пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр формы входа
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            textBox2.PasswordChar = '•'; // Устанавливаем маскировку пароля
            this.AcceptButton = loginBTN;
        }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private void loginBTN_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text.Trim();
            var password = textBox2.Text;

            // Валидация введенных данных
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Login == login);

                    if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                    {
                        var userId = user.Id;
                        Logger.Info($"Пользователь вошёл в приложение");

                        var anketaForm = new AnketaForm(userId);
                        anketaForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка:", ex);
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void registrationBTN_Click(object sender, EventArgs e)
        {
            var regForm = new RegistrationForm();
            if (regForm.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = regForm.Username;
                textBox2.Focus();
            }
        }

        private void newPassBTN_Click(object sender, EventArgs e)
        {
            var newPasswordForm = new NewPasswordForm();
            newPasswordForm.ShowDialog();
        }
    }
}