using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using NLog;
using Microsoft.EntityFrameworkCore;
namespace WinFormsApp2
{
    /// <summary>
    /// Форма восстановления пароля 
    /// </summary>
    public partial class NewPasswordForm : Form
    {
        //  SMTP-сервер
        private const string SmtpHost = "smtp.yandex.ru";
        private const int SmtpPort = 587;
        private const string SmtpUsername = "dana.khal@yandex.ru";
        private const string SmtpPassword = "ixcgnrwyfwmeljlo";
        private const string FromEmail = "dana.khal@yandex.ru";
        private const string FromName = "КиноПлюс";

        /// <summary>
        /// Код подтверждения
        /// </summary>
        public string SentCode { get; set; }

        /// <summary>
        /// Email для восстановления пароля
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Строка подключения к PostgreSQL
        /// </summary>
        public const string ConnectionString = "Server=localhost;Database=Userr;User Id=postgres;Password=2502;";

        /// <summary>
        /// Инициализирует новый экземпляр формы восстановления пароля
        /// </summary>
        public NewPasswordForm()
        {
            InitializeComponent();
        }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Action<string> MessageBoxShow { get; set; } = (text) => MessageBox.Show(text);

        public async void sendCodeBTN_Click(object sender, EventArgs e)
        {
            var userEmail = textBox1.Text.Trim();
            UserEmail = userEmail;
            Logger.Info($"Восстановление пароля для: {UserEmail}");

            // Валидация email
            if (!IsValidEmail(UserEmail))
            {
                Logger.Error("Ошибка: Неверный формат email");
                MessageBoxShow("Введите корректный email");
                return;
            }

            if (!UserExists(UserEmail))
            {
                Logger.Error($"Ошибка: пользователь не найден");
                MessageBoxShow($"Пользователь {UserEmail} не найден");
                return;
            }

            SentCode = new Random().Next(100000, 999999).ToString();
            Logger.Info($"Код подтверждения: {SentCode}");
            try
            {
                await SendEmailViaYandexSMTP(UserEmail, SentCode);
                Logger.Info($"Код отправлен на {UserEmail}");
                MessageBoxShow($"Код подтверждения отправлен на {UserEmail}");

                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                verificationBTN.Enabled = true;
                textBox2.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки email", ex);
                MessageBoxShow($"Ошибка: {ex.Message}");
            }
        }

        private async Task SendEmailViaYandexSMTP(string email, string code)
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

        private bool UserExists(string email)
        {
            try
            {
                var normalizedEmail = email.Trim().ToLower();

                using (var db = new AppDbContext())
                {
                    return db.Users.Any(u => u.Email.Trim().ToLower() == normalizedEmail);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Ошибка проверки пользователя по email");
                throw;
            }
        }

        private async void verificationBTN_Click(object sender, EventArgs e)
        {
            Logger.Info("Попытка верификации кода");

            if (textBox2.Text != SentCode)
            {
                Logger.Error("Неверный код");
                MessageBoxShow("Неверный код");
                textBox2.Focus();
                return;
            }

            if (textBox3.Text != textBox4.Text)
            {
                Logger.Error("Пароли не совпадают");
                MessageBoxShow("Пароли не совпадают");
                textBox3.Focus();
                return;
            }

            if (!IsPasswordValid(textBox3.Text))
            {
                Logger.Error("Некорректный формат пароля");
                MessageBoxShow("Пароль должен содержать минимум 6 символов, включая буквы");
                textBox3.Focus();
                return;
            }

            try
            {
                var success = await UpdatePassword(UserEmail, textBox3.Text);
                if (success)
                {
                    Logger.Error("Пароль изменён");
                    MessageBoxShow("Пароль изменён");
                    this.Close();
                }
                else
                {
                    Logger.Error("Не удалось изменить пароль");
                    MessageBoxShow("Не удалось изменить пароль");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка:", ex);
                MessageBoxShow($"Ошибка: {ex.Message}");
            }
        }

        private async Task<bool> UpdatePassword(string email, string newPassword)
        {
            try
            {
                var hashedPassword = HashPassword(newPassword);
                Logger.Info("Пароль хэширован");

                using (var context = new AppDbContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

                    if (user == null)
                        return false;

                    user.Password = hashedPassword;
                    context.Users.Update(user);
                    var affectedRows = await context.SaveChangesAsync();
                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка обновления пароля", ex);
                throw;
            }
        }

        private string HashPassword(string password)
        {
            try
            {
                var hash = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
                Logger.Info("Хэширование выполнено");
                return hash;
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка:", ex);
                throw;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                var isValid = addr.Address == email;
                Logger.Info($"Проверка email {email}: {(isValid ? "подходит" : "не подходит")}");
                return isValid;
            }
            catch
            {
                Logger.Error($"Email {email} не подходит");
                return false;
            }
        }

        private bool IsPasswordValid(string password)
        {
            var isValid = password.Length >= 6 &&
                          Regex.IsMatch(password, @"\d") &&
                          Regex.IsMatch(password, @"[a-zA-Z]");

            Logger.Info($"Проверка пароля: {(isValid ? "соответствует требованиям" : "не соответствует")}");
            return isValid;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}