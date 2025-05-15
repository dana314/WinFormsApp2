using Newtonsoft.Json;

namespace WinFormsApp2
{
    /// <summary>
    /// Класс для хранения данных о фильме
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Название фильма
        /// </summary>
        [JsonProperty("name")]
        public string Title { get; set; }

        /// <summary>
        /// Год выпуска 
        /// </summary>
        [JsonProperty("year")]
        public int Year { get; set; }

        /// <summary>
        /// Страны производства 
        /// </summary>
        [JsonProperty("country")]
        public string Countries { get; set; }

        /// <summary>
        /// Жанры фильма 
        /// </summary>
        [JsonProperty("genre")]
        public string Genres { get; set; }

        /// <summary>
        /// Возрастное ограничение 
        /// </summary>
        [JsonProperty("age")]
        public string Age { get; set; }

        /// <summary>
        /// URL постера фильма
        /// </summary>
        [JsonProperty("poster")]
        public string Poster { get; set; }
    }
}