using Newtonsoft.Json;
using System.Net;
namespace WinFormsApp2
{
    public class MovieAPI
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BaseUrl = "https://kinopoiskapiunofficial.tech/api/v2.2/films? ";
        private const string ApiKey = "2191b521-496b-4915-b7d0-340f911d8361";

        /// <summary>
        /// Получаем рекомендованные фильмы по фильтрам
        /// </summary>
        public static async Task<List<Movie>> GetRecommendedMovies(string genre, string country, int decade, string age, int limit)
        {
            try
            {
                var queryParams = new List<string>
                {
                    $"page=1",
                    $"limit={limit}"
                };

                if (!string.IsNullOrEmpty(genre))
                    queryParams.Add($"genres.genre={WebUtility.UrlEncode(genre)}");

                if (!string.IsNullOrEmpty(country))
                    queryParams.Add($"countries.country={WebUtility.UrlEncode(country)}");

                if (decade > 0)
                    queryParams.Add($"year={decade}-{decade + 9}");

                // ageRating пока не поддерживается напрямую через этот источник апи

                var requestUrl = BaseUrl + string.Join("&", queryParams);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                requestMessage.Headers.Add("X-API-KEY", ApiKey);

                var response = await client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(json))
                {
                    MessageBox.Show("Пустой ответ от API");
                    return new List<Movie>();
                }

                var result = JsonConvert.DeserializeObject<ApiResponse>(json);
                if (result == null || result.Items == null || !result.Items.Any())
                {
                    MessageBox.Show("API вернул пустой список фильмов");
                    return new List<Movie>();
                }

                return result.Items.Select(item => new Movie
                {
                    Title = item.NameOriginal ?? item.NameRu ?? item.NameEn ?? "Без названия",
                    Genres = string.Join(", ", item.Genres?.Select(g => g.Genre) ?? new List<string> { "Не указано" }),
                    Countries = string.Join(", ", item.Countries?.Select(c => c.Country) ?? new List<string> { "Не указана" }),
                    Year = item.Year,
                    Age =item.AgeRating?.Rating ?? null,
                    Poster = item.PosterUrl ?? null
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки фильмов: {ex.Message}");
                return new List<Movie>();
            }
        }

        /// <summary>
        /// Загрузка фильмов на основе предпочтений пользователя
        /// </summary>
        public static async Task<List<Movie>> LoadRecommendedMoviesFromPreferences(UserPreferences prefs, int limit = 5)
        {
            if (prefs == null)
            {
                MessageBox.Show("Объект UserPreferences не может быть null");
                return new List<Movie>();
            }

            return await GetRecommendedMovies(
                prefs.Genres ?? string.Empty,
                prefs.Countries ?? string.Empty,
                prefs.Decade ?? 0,
                prefs.Age ?? string.Empty,
                limit
            );
        }

        /// <summary>
        /// Кэширование изображений
        /// </summary>
        public static readonly Dictionary<string, Image> _imageCache = new();

        /// <summary>
        /// Загрузка изображений с  помощью URL
        /// </summary>
        public static Image LoadImage(string url)
        {
            if (string.IsNullOrEmpty(url))
                return Properties.Resources.Фон_авторизация;

            if (_imageCache.TryGetValue(url, out var cachedImage))
                return cachedImage;

            try
            {
                var request = WebRequest.Create(url);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    if (stream == null)
                        return Properties.Resources.Фон_авторизация;

                    var image = Image.FromStream(stream);
                    _imageCache[url] = image;
                    return image;
                }
            }
            catch
            {
                return Properties.Resources.Фон_авторизация;
            }
        }
    }
}