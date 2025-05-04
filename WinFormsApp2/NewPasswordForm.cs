using System.Net;
using System.Net.Mail;
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
            Logger.Log($"Всстановление пароля для: {userEmail}");

            if (!IsValidEmail(userEmail))
            {
                Logger.Log("Ошибка: Неверный формат email");
                MessageBox.Show("Введите корректный email");
                return;
            }

            if (!await UserExists(userEmail))
            {
                Logger.Log($"Ошибка: Пользователь не найден");
                MessageBox.Show($"Пользователь {userEmail} не найден");
                return;
            }
            sentCode = new Random().Next(100000, 999999).ToString();
            Logger.Log($"Код подтверждения: {sentCode}");
            try
            {
                await SendEmailViaYandexSMTP(userEmail, sentCode);
                Logger.Log($"Код отправлен на {userEmail}");
                MessageBox.Show($"Код подтверждения отправлен на {userEmail}");

                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                verificationBTN.Enabled = true;
                textBox2.Focus();
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка отправки email", ex);
                MessageBox.Show($"Ошибка: {ex.Message}");
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
                smtpClient.UseDefaultCredentials = false;

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
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    string query = @"SELECT COUNT(1) > 0 
                            FROM ""Users"" 
                            WHERE LOWER(TRIM(""Email"")) = LOWER(TRIM(@Email))";

                    return await connection.ExecuteScalarAsync<bool>(query, new { Email = email });
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка проверки", ex);
                throw;
            }
        }

        public async void verificationBTN_Click(object sender, EventArgs e)
        {
            Logger.Log("Попытка верификации кода");

            if (textBox2.Text != sentCode)
            {
                Logger.Log("Неверный код");
                MessageBox.Show("Неверный код");
                textBox2.Focus();
                return;
            }

            if (textBox3.Text != textBox4.Text)
            {
                Logger.Log("Пароли не совпадают");
                MessageBox.Show("Пароли не совпадают");
                textBox3.Focus();
                return;
            }

            if (!IsPasswordValid(textBox3.Text))
            {
                Logger.Log("Некорректный формат пароля");
                MessageBox.Show("Пароль должен содержать минимум 6 символов");
                textBox3.Focus();
                return;
            }

            try
            {
                bool success = await UpdatePassword(userEmail, textBox3.Text);
                if (success)
                {
                    Logger.Log("Пароль изменен");
                    MessageBox.Show("Пароль изменен");
                    this.Close();
                }
                else
                {
                    Logger.Log("Не удалось изменить пароль");
                    MessageBox.Show("Не удалось изменить пароль");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка:", ex);
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        public async Task<bool> UpdatePassword(string email, string newPassword)
        {
            try
            {
                string hashedPassword = HashPassword(newPassword);
                Logger.Log("Пароль хэширован");

                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    string query = "UPDATE Users SET PasswordHash = @PasswordHash WHERE Email = @Email";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        int affectedRows = await command.ExecuteNonQueryAsync();
                        return affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка обновления пароля", ex);
                throw;
            }
        }

        public string HashPassword(string password)
        {
            try
            {
                var hash = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
                Logger.Log("Хэширование выполнено");
                return hash;
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка:", ex);
                throw;
            }
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                bool isValid = addr.Address == email;
                Logger.Log($"Проверка email {email}: {(isValid ? "подходит" : "не подходит")}");
                return isValid;
            }
            catch
            {
                Logger.Log($"Email {email} не подходит");
                return false;
            }
        }

        public bool IsPasswordValid(string password)
        {
            bool isValid = password.Length >= 6 &&
                          Regex.IsMatch(password, @"\d") &&
                          Regex.IsMatch(password, @"[a-zA-Z]");

            Logger.Log($"Проверка пароля: {(isValid ? "соответствует требованиям" : "не соответствует")}");
            return isValid;
        }
        public void textBox1_TextChanged(object sender, EventArgs e) { }

        public void textBox2_TextChanged(object sender, EventArgs e) { }

        public void textBox3_TextChanged(object sender, EventArgs e) {}

        public void textBox4_TextChanged(object sender, EventArgs e) { }

        public void label1_Click(object sender, EventArgs e) { }
        public void label2_Click(object sender, EventArgs e) { }
        public void label3_Click(object sender, EventArgs e) { }
        public void label4_Click(object sender, EventArgs e) { }
        public void label5_Click(object sender, EventArgs e) { }

        public void label1_Click_1(object sender, EventArgs e) { }
        public void label2_Click_1(object sender, EventArgs e) { }
    }
}