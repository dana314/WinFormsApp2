using NLog;
namespace WinFormsApp2
{
    /// <summary>
    /// Главная форма: рекомендации для пользователя
    /// </summary>
    public partial class MainForm : Form
    {
        public const int CARD_WIDTH = 250;
        public const int CARD_MARGIN = 15;
        public const int CARDS_PER_LOAD = 5;

        private readonly UserPreferences _preferences;
        private readonly List<Movie> _initialMovies;
        private readonly List<Control[]> _movieCards = new();
        private User _currentUser;
        private int _currentPage = 1;
        private bool _isLoading;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Конструктор 1
        /// </summary>
        public MainForm(List<Movie> initialMovies, UserPreferences preferences)
        {
            _initialMovies = initialMovies ?? new List<Movie>();
            _preferences = preferences;
            InitializeComponent();
            Load += OnLoad;
        }

        /// <summary>
        /// Конструктор 2
        /// </summary>
        public MainForm(User user, UserPreferences preferences) : this(preferences)
        {
            _currentUser = user;
        }

        /// <summary>
        /// Конструктор 3
        /// </summary>
        public MainForm(UserPreferences preferences)
        {
            _preferences = preferences;
            InitializeComponent();
            Load += OnLoad;
        }

        private void InitializeComponent()
        {
            this.Text = "Рекомендации по фильмам";
            this.Width = 1000;
            this.Height = 600;

            var topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.Brown
            };

            // Кнопка "Обновить анкету"
            var btnUpdateAnketa = new Button
            {
                Text = "Обновить Анкету",
                Width = 200,
                Height = 40,
                Location = new Point(600, 8),
                Font = new Font("Impact", 12)
            };
            btnUpdateAnketa.Click += async (s, e) => await UpdateUserPreferencesAsync();
            topPanel.Controls.Add(btnUpdateAnketa);

            // Панель для отображения фильмов
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            // Горизонтальная прокрутка
            var hScrollBar = new HScrollBar
            {
                Dock = DockStyle.Bottom,
                Minimum = 0,
                SmallChange = 20,
                LargeChange = 200
            };
            hScrollBar.Scroll += (s, e) => UpdateCardsPosition(hScrollBar.Value, panel);

            // Кнопка "Загрузить ещё"
            var btnPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.Brown,
                Font = new Font("Impact", 12)
            };
            var btnRecommend = new Button
            {
                Text = "Загрузить ещё",
                Width = 150,
                Height = 40,
                Location = new Point(800, 5)
            };
            btnRecommend.Click += async (s, e) => await LoadRecommendedMoviesAsync(panel);
            btnPanel.Controls.Add(btnRecommend);

            this.Controls.Add(topPanel);
            this.Controls.Add(hScrollBar);
            this.Controls.Add(btnPanel);
            this.Controls.Add(panel);
        }

        private async void OnLoad(object sender, EventArgs e)
        {
            await LoadRecommendedMoviesAsync(null);
        }

        private async Task LoadRecommendedMoviesAsync(Panel? panel = null)
        {
            if (_isLoading) return;
            _isLoading = true;
            panel ??= GetMoviePanel();
            if (panel == null)
            {
                Logger.Error("Нет панели для отображения фильмов");
                MessageBox.Show("Не найдена панель для отображения фильмов");
                return;
            }

            try
            {
                var prefs = GetUserPreferencesFromMovies(_initialMovies);
                if (!prefs.IsValid)
                {
                    Logger.Info("Пользователь указывает свои предпочтения");
                    MessageBox.Show("Укажите свои предпочтения");
                    using (var anketa2 = new Anketa2())
                    {
                        if (anketa2.ShowDialog() == DialogResult.OK)
                        {
                            var selectedFromAnketa2 = anketa2.GetSelectedMovies();
                            prefs = AnalyzePreferences(_initialMovies, selectedFromAnketa2);
                        }
                        else
                        {
                            Logger.Error("Пользователь ничего не указал");
                            throw new Exception("Необходимо указать предпочтения");
                        }
                    }
                }

                var recommendedMovies = await MovieAPI.GetRecommendedMovies(
                    prefs.Genres,
                    prefs.Countries,
                    prefs.Decade ?? 0,
                    prefs.Age,
                    CARDS_PER_LOAD * 5
                );

                if (!recommendedMovies.Any())
                {
                    Logger.Info("По вашим предпочтениям ничего не найдено");
                    MessageBox.Show("По вашим предпочтениям ничего не найдено");
                    return;
                }

                // Коэффициенты
                var scoredMovies = recommendedMovies
                    .Select(movie => new { Movie = movie, Score = CalculateRelevance(movie, prefs) })
                    .Where(x => x.Score > 0)
                    .OrderByDescending(x => x.Score)
                    .Take(CARDS_PER_LOAD)
                    .Select(x => x.Movie)
                    .ToList();

                DisplayMovies(scoredMovies, panel);
                _currentPage++;
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка", ex);
                MessageBox.Show($"Ошибка загрузки фильмов: {ex.Message}");
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async Task UpdateUserPreferencesAsync()
        {
            using (var anketa = new Anketa2(this))
            {
                if (anketa.ShowDialog(this) == DialogResult.OK)
                {
                    Logger.Info("Предпочтения пользователя обновлены");
                }
            }
        }

        private Panel GetMoviePanel()
        {
            return this.Controls.OfType<Panel>().FirstOrDefault(p => p.AutoScroll);
        }

        /// <summary>
        /// Обновление рекомендаций
        /// </summary>
        public async void UpdateRecommendations(UserPreferences updatedPrefs)
        {
            var panel = GetMoviePanel();
            if (panel == null) return;

            ClearMovieCards(panel);

            var recommendedMovies = await MovieAPI.GetRecommendedMovies(
                updatedPrefs.Genres,
                updatedPrefs.Countries,
                updatedPrefs.Decade ?? 0,
                updatedPrefs.Age,
                CARDS_PER_LOAD
            );

            if (recommendedMovies.Any())
            {
                DisplayMovies(recommendedMovies, panel);
                _currentPage = 1;
            }
            else
            {
                Logger.Info("Нет фильмов по обновленным предпочтениям");
                MessageBox.Show("По вашим новым предпочтениям ничего не найдено");
            }

            _preferences.UpdateFrom(updatedPrefs);
        }

        private void ClearMovieCards(Panel panel)
        {
            foreach (var card in _movieCards)
            {
                foreach (var control in card)
                {
                    panel.Controls.Remove(control);
                }
            }
            _movieCards.Clear();
        }

        #region Расчёт совпадений
        private double CalculateRelevance(Movie movie, UserPreferences prefs)
        {
            double score = 0;
            const double GenreWeight = 0.5;
            const double CountryWeight = 0.2;
            const double DecadeWeight = 0.2;
            const double AgeWeight = 0.1;

            if (!string.IsNullOrEmpty(prefs.Genres) && !string.IsNullOrEmpty(movie.Genres) &&
                prefs.Genres.Equals(movie.Genres, StringComparison.OrdinalIgnoreCase))
            {
                score += GenreWeight;
            }

            if (!string.IsNullOrEmpty(prefs.Countries) && !string.IsNullOrEmpty(movie.Countries) &&
                prefs.Countries.Equals(movie.Countries, StringComparison.OrdinalIgnoreCase))
            {
                score += CountryWeight;
            }

            if (prefs.Decade.HasValue && movie.Year >= prefs.Decade && movie.Year < prefs.Decade + 10)
            {
                score += DecadeWeight;
            }

            if (!string.IsNullOrEmpty(prefs.Age) && !string.IsNullOrEmpty(movie.Age) &&
                prefs.Age.Equals(movie.Age, StringComparison.OrdinalIgnoreCase))
            {
                score += AgeWeight;
            }

            return score;
        }
        #endregion

        private UserPreferences GetUserPreferencesFromMovies(List<Movie> movies)
        {
            return new UserPreferences
            {
                Genres = GetTopGenre(movies),
                Countries = GetTopCountry(movies),
                Decade = GetTopDecade(movies),
                Age = GetTopAge(movies)
            };
        }

        private UserPreferences AnalyzePreferences(List<Movie> anketa1, List<Movie> anketa2)
        {
            var allMovies = anketa1.Concat(anketa2).ToList();
            return new UserPreferences
            {
                Genres = GetTopGenre(allMovies),
                Countries = GetTopCountry(allMovies),
                Decade = GetTopDecade(allMovies),
                Age = GetTopAge(allMovies)
            };
        }

        private string GetTopGenre(List<Movie> movies) =>
            movies.Where(m => !string.IsNullOrEmpty(m.Genres))
                  .GroupBy(m => m.Genres)
                  .OrderByDescending(g => g.Count())
                  .Select(g => g.Key)
                  .FirstOrDefault();

        private string GetTopCountry(List<Movie> movies) =>
            movies.Where(m => !string.IsNullOrEmpty(m.Countries))
                  .GroupBy(m => m.Countries)
                  .OrderByDescending(g => g.Count())
                  .Select(g => g.Key)
                  .FirstOrDefault();

        private int? GetTopDecade(List<Movie> movies) =>
            movies.Where(m => m.Year > 0)
                  .Select(m => m.Year / 10 * 10)
                  .GroupBy(y => y)
                  .OrderByDescending(g => g.Count())
                  .Select(g => (int?)g.Key)
                  .FirstOrDefault();

        private string GetTopAge(List<Movie> movies) =>
            movies.Where(m => !string.IsNullOrEmpty(m.Age))
                  .GroupBy(m => m.Age)
                  .OrderByDescending(g => g.Count())
                  .Select(g => g.Key)
                  .FirstOrDefault();

        private void DisplayMovies(IEnumerable<Movie> movies, Panel panel)
        {
            int startX = _movieCards.Count > 0 ? GetLastCardRight() + CARD_MARGIN : CARD_MARGIN;
            foreach (var movie in movies)
            {
                var card = CreateMovieCard(movie, startX);
                panel.Controls.AddRange(card);
                _movieCards.Add(card);
                startX += CARD_WIDTH + CARD_MARGIN;
            }
            UpdateScrollBar(panel);
        }

        private Control[] CreateMovieCard(Movie movie, int xPos)
        {
            var picPoster = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(CARD_WIDTH - 20, 300),
                Location = new Point(xPos + 10, 10),
                Image = MovieAPI.LoadImage(movie.Poster),
                Tag = xPos,
                Cursor = Cursors.Hand
            };
            picPoster.Click += (s, e) => ShowMovieDetails(movie);

            var lblTitle = new Label
            {
                Font = new Font("Arial", 10, FontStyle.Bold),
                Text = $"{movie.Title} ({movie.Year})",
                Location = new Point(xPos + 10, 320),
                Size = new Size(CARD_WIDTH - 20, 20),
                ForeColor = Color.White
            };

            var lblGenre = new Label
            {
                Text = $"Жанр: {movie.Genres}",
                Location = new Point(xPos + 10, 345),
                Size = new Size(CARD_WIDTH - 20, 15),
                ForeColor = Color.LightGray
            };

            var lblCountry = new Label
            {
                Text = $"Страна: {movie.Countries}",
                Location = new Point(xPos + 10, 360),
                Size = new Size(CARD_WIDTH - 20, 15),
                ForeColor = Color.LightGray
            };

            return new Control[] { picPoster, lblTitle, lblGenre, lblCountry };
        }

        private void UpdateCardsPosition(int scrollValue, Panel panel)
        {
            foreach (var card in _movieCards)
            {
                foreach (var control in card)
                {
                    control.Left = control.Tag is int originalX
                        ? originalX - scrollValue
                        : control.Left;
                }
            }
        }

        private void UpdateScrollBar(Panel panel)
        {
            if (_movieCards.Count == 0) return;
            int totalWidth = GetLastCardRight() + CARD_MARGIN;
            int visibleWidth = panel.ClientSize.Width;
            int maxScroll = Math.Max(0, totalWidth - visibleWidth);

            var hScrollBar = this.Controls.OfType<HScrollBar>().FirstOrDefault();
            if (hScrollBar != null)
            {
                hScrollBar.Maximum = maxScroll;
                hScrollBar.Enabled = totalWidth > visibleWidth;
            }
        }

        private int GetLastCardRight() =>
            _movieCards.Count == 0 ? 0 : _movieCards.Max(c => c.Max(x => x.Right));

        private void ShowMovieDetails(Movie movie)
        {
            var detailsForm = new Form
            {
                Text = movie.Title,
                Size = new Size(600, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            var posterBox = new PictureBox
            {
                Dock = DockStyle.Top,
                Height = 300,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = MovieAPI.LoadImage(movie.Poster)
            };

            var infoLabel = new Label
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                Font = new Font("Arial", 10),
                Text = $@"Название: {movie.Title}
                       Год: {movie.Year}
                       Жанр: {movie.Genres}
                       Страна: {movie.Countries}
                       Возраст: {movie.Age}"
            };

            detailsForm.Controls.Add(infoLabel);
            detailsForm.Controls.Add(posterBox);
            detailsForm.ShowDialog(this);
        }
    }
}