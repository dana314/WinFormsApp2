using NLog;
namespace WinFormsApp2
{
    /// <summary>
    /// Форма 2 анкеты пользователя 
    /// </summary>
    public partial class Anketa2 : Form
    {
        private readonly MovieAPI _movieApi = new MovieAPI();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private MainForm _mainForm;

        /// <summary>
        /// Инициализирует новый экземпляр формы анкеты
        /// </summary>
        public Anketa2()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        public Anketa2(MainForm mainForm) : this()
        {
            _mainForm = mainForm;
        }

        /// <summary>
        /// Возвращает список фильмов, соответствующих критериям
        /// </summary>
        public List<Movie> GetSelectedMovies()
        {
            return new List<Movie>
            {
                new Movie { Title = "Фильм 1", Genres = "Комедия", Year = 2020, Countries = "США", Age = "12+" },
                new Movie { Title = "Фильм 2", Genres = "Драма", Year = 2019, Countries = "Россия", Age = "16+" }
            };
        }

        private void InitializeComponentt()
        {
            this.Text = "Анкета 2";
            this.Width = 400;
            this.Height = 300;

            var button1 = new Button { Text = "К рекомендациям", Location = new Point(100, 200) };
            var comboBox1 = new ComboBox { Location = new Point(100, 20), Width = 200 };
            var comboBox2 = new ComboBox { Location = new Point(100, 50), Width = 200 };
            var comboBox3 = new ComboBox { Location = new Point(100, 80), Width = 200 };
            var comboBox4 = new ComboBox { Location = new Point(100, 110), Width = 200 };

            button1.Click += button1_Click;

            this.Controls.Add(comboBox1);
            this.Controls.Add(comboBox2);
            this.Controls.Add(comboBox3);
            this.Controls.Add(comboBox4);
            this.Controls.Add(button1);
        }

        private void InitializeComboBoxes()
        {
            // Жанры
            comboBox1.Items.AddRange(new object[]
            {
                "Комедия", "Мелодрама", "Ужасы", "Триллер",
                "Детектив", "Фантастика", "Боевик", "Мультфильм"
            });

            // Страны
            comboBox2.Items.AddRange(new object[]
            {
                "США", "Великобритания", "Канада", "Франция", "Германия", "Италия",
                "Испания", "Австралия", "Новая Зеландия", "Япония", "Южная Корея",
                "Китай", "Индия", "Россия"
            });

            // Десятилетия
            comboBox3.Items.AddRange(new object[]
            {
                "1920", "1930", "1940", "1950", "1960", "1970", "1980",
                "1990", "2000", "2010", "2020"
            });

            // Возрастной рейтинг
            comboBox4.Items.AddRange(new object[] { "0+", "6+", "12+", "16+", "18+" });
        }

        private bool ValidateSelections()
        {
            if (comboBox1.SelectedItem == null)
                MessageBox.Show("Выберите жанр");
            else if (comboBox2.SelectedItem == null)
                MessageBox.Show("Выберите страну");
            else if (comboBox3.SelectedItem == null)
                MessageBox.Show("Выберите десятилетие");
            else if (comboBox4.SelectedItem == null)
                MessageBox.Show("Выберите возрастной рейтинг");

            return comboBox1.SelectedItem != null &&
                   comboBox2.SelectedItem != null &&
                   comboBox3.SelectedItem != null &&
                   comboBox4.SelectedItem != null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateSelections())
                return;

            var preferences = GetUserPreferences();

            if (_mainForm != null)
            {
                _mainForm.UpdateRecommendations(preferences);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var mainForm = new MainForm(preferences);
                mainForm.Show();
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        /// <summary>
        /// Собирает предпочтения 
        /// </summary>
        public UserPreferences GetUserPreferences()
        {
            return new UserPreferences
            {
                Genres = comboBox1.SelectedItem?.ToString(),
                Countries = comboBox2.SelectedItem?.ToString(),
                Decade = int.TryParse(comboBox3.SelectedItem?.ToString(), out var d) ? d : 0,
                Age = comboBox4.SelectedItem?.ToString()
            };
        }

    }
}