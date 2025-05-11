namespace WinFormsApp2
{
    public partial class LoginForm : Form
    {
        public Action<string> MessageBoxShow { get; set; } = (text) => MessageBox.Show(text);
        public LoginForm()
        {
            InitializeComponent();


            textBox2.PasswordChar = 'Х';
            this.AcceptButton = loginBTN;
        }

        public void loginBTN_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBoxShow("¬ведите номер и пароль");
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Login == login);

                    if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                    {
                        new MainForm().Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBoxShow("ѕароль неверный");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxShow($"ќшибка: {ex.Message}");
            }
        }

        private void registrationBTN_Click(object sender, EventArgs e)
        {
            new RegistrationForm().Show();
        }
        public void newPassBTN_Click(object sender, EventArgs e)
        {
            new NewPasswordForm().Show();
        }
    }
}