using System.ComponentModel.DataAnnotations;

namespace WinFormsApp2
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор  
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Логин  (номер телефона)
        /// </summary>
        [Required(ErrorMessage = "Номер телефона обязателен")]
        [StringLength(20)]
        [RegularExpression(@"^\+[0-9]{11,20}$", ErrorMessage = "Введите номер телефона (можно с +)")]
        public string Login { get; set; }

        /// <summary>
        /// Имя 
        /// </summary>
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        [StringLength(15)]
        public string UserName { get; set; }

        /// <summary>
        /// Электронная почта 
        /// </summary>
        [Required(ErrorMessage = "Почта обязательна")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Пароль 
        /// </summary>
        [Required(ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; }

        /// <summary>
        /// Дата рождения 
        /// </summary>
        [Required(ErrorMessage = "Дата рождения обязательна")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Временный код для сброса пароля 
        /// </summary>
        public string? NewCode { get; set; }

        /// <summary>
        /// Срок действия временного кода 
        /// </summary>
        public DateTime? DurationOfNewCode { get; set; }
    }
}