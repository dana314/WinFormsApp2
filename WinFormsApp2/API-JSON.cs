/// <summary>
/// ответ API с данными о фильмах
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Количество найденных фильмов
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// Количество страниц с результатами
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Список фильмов на текущей странице
    /// </summary>
    public List<ApiMovieItem> Items { get; set; }
}

/// <summary>
/// Информацию о фильме
/// </summary>
public class ApiMovieItem
{
    /// <summary>
    /// ID фильма в базе Кинопоиска
    /// </summary>
    public int KinopoiskId { get; set; }

    /// <summary>
    /// Название фильма на русском языке
    /// </summary>
    public string NameRu { get; set; }

    /// <summary>
    /// Название фильма на английском языке
    /// </summary>
    public string NameEn { get; set; }

    /// <summary>
    /// Оригинальное название фильма
    /// </summary>
    public string NameOriginal { get; set; }

    /// <summary>
    /// Год выпуска 
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Список жанров
    /// </summary>
    public List<ApiGenre> Genres { get; set; }

    /// <summary>
    /// Список стран производства 
    /// </summary>
    public List<ApiCountry> Countries { get; set; }

    /// <summary>
    /// URL постера фильма 
    /// </summary>
    public string PosterUrl { get; set; }

    /// <summary>
    /// URL постера фильма (превью)
    /// </summary>
    public string PosterUrlPreview { get; set; }

    /// <summary>
    /// Возрастной рейтинг
    /// </summary>
    public ApiAgeRating AgeRating { get; set; }
}

/// <summary>
/// Жанр фильма
/// </summary>
public class ApiGenre
{
    /// <summary>
    /// Название жанра
    /// </summary>
    public string Genre { get; set; }
}

/// <summary>
/// Страна производства
/// </summary>
public class ApiCountry
{
    /// <summary>
    /// Название страны
    /// </summary>
    public string Country { get; set; }
}

/// <summary>
/// Возрастной рейтинг
/// </summary>
public class ApiAgeRating
{
    /// <summary>
    /// Значение возрастного рейтинга
    /// </summary>
    public string Rating { get; set; }
}