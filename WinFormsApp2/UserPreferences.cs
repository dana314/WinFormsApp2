namespace WinFormsApp2
{
    /// <summary>
    /// Класс предпочтений пользователя 
    /// </summary>
    public class UserPreferences
    {
        /// <summary>
        /// Идентификатор 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Предпочитаемые жанры фильмов 
        /// </summary>
        public string Genres { get; set; }

        /// <summary>
        /// Предпочитаемые страны производства 
        /// </summary>
        public string Countries { get; set; }

        /// <summary>
        /// Предпочитаемое десятилетие фильмов
        /// </summary>
        public int? Decade { get; set; }

        /// <summary>
        /// Предпочитаемые возрастные ограничения
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// Проверяет, есть ли хотя бы одно предпочтение
        /// </summary>
        public bool IsValid => !string.IsNullOrEmpty(Genres) ||
                               !string.IsNullOrEmpty(Countries) ||
                               Decade > 0;

        /// <summary>
        /// Обновляет текущие предпочтения 
        /// </summary>
        public void UpdateFrom(UserPreferences other)
        {
            Genres = other.Genres;
            Countries = other.Countries;
            Decade = other.Decade;
            Age = other.Age;
        }
    }
}