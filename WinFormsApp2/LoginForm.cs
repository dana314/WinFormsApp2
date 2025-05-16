using NLog;
namespace WinFormsApp2
{
    /// <summary>
    /// ����� ����������� ������������ � �������
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// ID �������� ��������������� ������������
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// �������������� ����� ��������� ����� �����
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            textBox2.PasswordChar = '�'; // ������������� ���������� ������
            this.AcceptButton = loginBTN;
        }

        /// <summary>
        /// ��������� �������� ����� � message box
        /// </summary>
        //����� ��� ���� ������ �� �������
        public Action<string> MessageBoxShow { get; set; } = (text) => MessageBox.Show(text);

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void loginBTN_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text.Trim();
            var password = textBox2.Text;

            // ��������� ��������� ������
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBoxShow("������� ����� � ������");
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
                        Logger.Info($"������������ ����� � ����������");

                        var anketaForm = new AnketaForm(userId);
                        anketaForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBoxShow("�������� ����� ��� ������");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("������:", ex);
                MessageBoxShow($"������: {ex.Message}");
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}