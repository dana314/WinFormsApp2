using NLog;
namespace WinFormsApp2
{
    /// <summary>
    /// Форма 1 анкеты для пользователя
    /// </summary>
    public partial class AnketaForm : Form
    {

        private List<PictureBox> SelectedPictureBoxes { get; } = new List<PictureBox>();


        private Dictionary<PictureBox, int> MovieMappings { get; } = new Dictionary<PictureBox, int>();


        private int CurrentUserId { get; }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Инициализирует экземпляр
        /// </summary>
        public AnketaForm(int userId)
        {
            InitializeComponent();
            CurrentUserId = userId;

            InitializeMovie();       
            InitializeMovieData();  
            SetupPictureBoxes();     
        }

        private void InitializeMovieData()
        {
            AddMovie(pictureBox1, label32.Text, label36.Text, label33.Text, label34.Text, label35.Text);
            AddMovie(pictureBox2, labelTitle2.Text, labelGenre2.Text, labelYear2.Text, labelCountry2.Text, labelAge2.Text);
            AddMovie(pictureBox3, labelTitle3.Text, labelGenre3.Text, labelYear3.Text, labelCountry3.Text, labelAge3.Text);
            AddMovie(pictureBox4, labelT4.Text, labelG4.Text, labelY4.Text, labelC4.Text, labelA4.Text);
            AddMovie(pictureBox5, labelT5.Text, labelG5.Text, labelY5.Text, labelC5.Text, labelA5.Text);
            AddMovie(pictureBox6, labelT6.Text, labelG6.Text, labelY6.Text, labelC6.Text, labelA6.Text);
            AddMovie(pictureBox7, labelT7.Text, labelG7.Text, labelY7.Text, labelC7.Text, labelA7.Text);
            AddMovie(pictureBox8, labelT8.Text, labelG8.Text, labelY8.Text, labelC8.Text, labelA8.Text);
            AddMovie(pictureBox9, labelT9.Text, labelG9.Text, labelY9.Text, labelC9.Text, labelA9.Text);
            AddMovie(pictureBox10, labelT10.Text, labelG10.Text, labelY10.Text, labelC10.Text, labelA10.Text);
        }

        private void AddMovie(PictureBox pb, string title, string genre, string yearStr, string country, string age)
        {
            if (!int.TryParse(yearStr, out int year))
                year = 0;

            var movie = new Movie
            {
                Title = title,
                Genres = genre,
                Year = year,
                Countries = country,
                Age = age
            };

            pb.Tag = movie;
        }

        private void InitializeMovie()
        {
            MovieMappings[pictureBox1] = 1;
            MovieMappings[pictureBox2] = 2;
            MovieMappings[pictureBox3] = 3;
            MovieMappings[pictureBox4] = 4;
            MovieMappings[pictureBox5] = 5;
            MovieMappings[pictureBox6] = 6;
            MovieMappings[pictureBox7] = 7;
            MovieMappings[pictureBox8] = 8;
            MovieMappings[pictureBox9] = 9;
            MovieMappings[pictureBox10] = 10;
        }

        private void SetupPictureBoxes()
        {
            foreach (Control control in panelFilms.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    pictureBox.Click += PictureBox_Click;
                    pictureBox.Cursor = Cursors.Hand;
                }
            }

            foreach (Control control in panelSerials.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    pictureBox.Click += PictureBox_Click;
                    pictureBox.Cursor = Cursors.Hand;
                }
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;

            if (clickedPictureBox.BorderStyle == BorderStyle.None)
            {
                clickedPictureBox.BorderStyle = BorderStyle.Fixed3D;
                clickedPictureBox.BackColor = Color.Brown;
                SelectedPictureBoxes.Add(clickedPictureBox);
            }
            else
            {
                clickedPictureBox.BorderStyle = BorderStyle.None;
                clickedPictureBox.BackColor = SystemColors.WindowFrame;
                SelectedPictureBoxes.Remove(clickedPictureBox);
            }
        }

        private List<Movie> GetSelectedMovies()
        {
            return SelectedPictureBoxes
                .Where(pb => pb.Tag is Movie)
                .Select(pb => (Movie)pb.Tag)
                .ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelFilms.Visible = false;
            panelSerials.Visible = true;
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panelSerials.Visible = false;
            panelFilms.Visible = true;
            button2.Enabled = true;
            button1.Enabled = false;
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
        private UserPreferences GetUserPreferences()
        {
            var selectedMovies = GetSelectedMovies();

            if (!selectedMovies.Any())
            {
                Logger.Info($"Пользователь ничего не выбрал");
                return new UserPreferences(); // Возвращаем пустые предпочтения, если ничего не выбрано
            }

            return new UserPreferences
            {
                Genres = GetTopGenre(selectedMovies),
                Countries = GetTopCountry(selectedMovies),
                Decade = GetTopDecade(selectedMovies),
                Age = GetTopAge(selectedMovies)
            };
        }

        private void NextForm()
        {
            if (SelectedPictureBoxes.Count > 0)
            {
                var selectedMovies = GetSelectedMovies();
                var mainForm = new MainForm(selectedMovies, GetUserPreferences());
                mainForm.Show();
            }
            else
            {
                using (var anketa2 = new Anketa2())
                {
                    if (anketa2.ShowDialog() == DialogResult.OK)
                    {
                        var prefs = anketa2.GetUserPreferences();

                        if (prefs != null && !string.IsNullOrEmpty(prefs.Genres))
                        {
                            var recommendedMovies = MovieAPI.LoadRecommendedMoviesFromPreferences(prefs).Result;

                            var mainForm = new MainForm(recommendedMovies, prefs);
                            mainForm.Show();
                        }
                        else
                        {
                            Logger.Info($"Пользователь ничего не выбрал");
                            MessageBox.Show("Не указаны предпочтения");
                        }
                    }
                }
            }

            this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            NextForm();
        }

        
    }
}