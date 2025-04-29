using System.ComponentModel.DataAnnotations;
namespace WinFormsApp2
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Номер телефона обязателен")]
        [StringLength(20)]
        [RegularExpression(@"+[0-9]{11,20}$",
            ErrorMessage = "Введите номер телефона ( можно с +)")]
        public string Login { get; set; } 
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        [StringLength(15)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Почта обязательна")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
       
        public string Password { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательна")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        //  для восстановления пароля
        public string? NewCode { get; set; }
        public DateTime? DurationOfNewCode { get; set; }
    }
}