namespace WinFormsApp2
{
    /// <summary>
    /// Элемент управления Panel для отображения информации о фильме
    /// </summary>
    public class MovieInfo : Panel
    {
        public MovieInfo(Movie movie)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;

            var poster = new PictureBox
            {
                Size = new Size(180, 120),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
            };

            var titleLabel = new Label
            {
                Text = movie.Title,
                Location = new Point(10, 140),
                AutoSize = true
            };

            this.Controls.Add(poster);
            this.Controls.Add(titleLabel);
        }
    }
}
