using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using Npgsql;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace WinFormsApp2
{
    public partial class NewPasswordForm : Form
    {
        private const string SmtpHost = "smtp.yandex.ru";
        private const int SmtpPort = 465;
        private const string SmtpUsername = "dana.khal@yandex.ru";
        private const string SmtpPassword = "ixcgnrwyfwmeljlo";
        private const string FromEmail = "dana.khal@yandex.ru";
        private const string FromName = "КиноПлюс";

        public string sentCode;
        public string userEmail;
        public const string ConnectionString = "Server=localhost;Database=Userr;User Id=postgres;Password=2502;";

        public NewPasswordForm()
        {
            InitializeComponent();
        }

        public async void sendCodeBTN_Click(object sender, EventArgs e)
        {
            userEmail = textBox1.Text.Trim();

            if (!IsValidEmail(userEmail))
            {
                MessageBox.Show("Введите корректный email");
                return;
            }

            if (!await UserExists(userEmail))
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }

            sentCode = new Random().Next(100000, 999999).ToString();

            try
            {
                await SendEmailViaYandexSMTP(userEmail, sentCode);
                MessageBox.Show($"Код подтверждения отправлен на {userEmail}");
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                verificationBTN.Enabled = true;
                textBox2.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\nПроверьте подключение к интернету и настройки SMTP.");
            }
        }

        public async Task SendEmailViaYandexSMTP(string email, string code)
        {
            using (var smtpClient = new SmtpClient(SmtpHost, SmtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(SmtpUsername, SmtpPassword);
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Timeout = 15000;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(FromEmail, FromName),
                    Subject = "Код подтверждения для смены пароля",
                    Body = $@"<h2>Восстановление пароля</h2>
                            <p>Ваш код подтверждения: <strong>{code}</strong></p>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);
                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        public async Task<bool> UserExists(string email)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                string query = @"SELECT COUNT(1) > 0 
                        FROM ""Users"" 
                        WHERE LOWER(TRIM(""Email"")) = LOWER(TRIM(@Email))";

                return await connection.ExecuteScalarAsync<bool>(query, new { Email = email });
            }
        }

        public async void verificationBTN_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != sentCode)
            {
                MessageBox.Show("Неверный код");
                textBox2.Focus();
                return;
            }

            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Пароли не совпадают");
                textBox3.Focus();
                return;
            }

            if (!IsPasswordValid(textBox3.Text))
            {
                MessageBox.Show("Пароль должен содержать минимум 6 символов, цифры и буквы");
                textBox3.Focus();
                return;
            }

            try
            {
                bool success = await UpdatePassword(userEmail, textBox3.Text);
                MessageBox.Show(success ? "Пароль изменен" : "Не удалось изменить пароль");
                if (success) this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        public async Task<bool> UpdatePassword(string email, string newPassword)
        {
            string hashedPassword = HashPassword(newPassword);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Users SET PasswordHash = @PasswordHash WHERE Email = @Email";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsPasswordValid(string password)
        {
            return password.Length >= 6 &&
                   Regex.IsMatch(password, @"\d") &&
                   Regex.IsMatch(password, @"[a-zA-Z]");
        }

        public void textBox1_TextChanged(object sender, EventArgs e) { }
        public void textBox2_TextChanged(object sender, EventArgs e) { }
        public void textBox3_TextChanged(object sender, EventArgs e) { }
        public void textBox4_TextChanged(object sender, EventArgs e) { }
        public void label1_Click(object sender, EventArgs e) { }
        public void label2_Click(object sender, EventArgs e) { }
        public void label3_Click(object sender, EventArgs e) { }
        public void label4_Click(object sender, EventArgs e) { }
        public void label5_Click(object sender, EventArgs e) { }

    }
}