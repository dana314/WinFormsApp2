namespace WinFormsApp2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            
            textBox2.PasswordChar = '�';
            this.AcceptButton = loginBTN; 
        }

        private void loginBTN_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("������� ����� � ������");
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
                        MessageBox.Show("������ ��������");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������: {ex.Message}");
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