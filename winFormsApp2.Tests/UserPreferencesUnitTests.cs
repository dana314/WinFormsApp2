using WinFormsApp2;

namespace winFormsApp2.Tests
{
    public class UserPreferencesUnitTests
    {
        [Fact]
        public void IsValid_NoPreferencesSet_ReturnsFalse()
        {
            UserPreferences preferences = new UserPreferences();

            bool isValid = preferences.IsValid;

            Assert.False(isValid);
        }

        [Fact]
        public void IsValid_GenresSet_ReturnsTrue()
        {
            UserPreferences preferences = new UserPreferences { Genres = "Action,Comedy" };

            bool isValid = preferences.IsValid;

            Assert.True(isValid);
        }

        [Fact]
        public void IsValid_CountriesSet_ReturnsTrue()
        {
            UserPreferences preferences = new UserPreferences { Countries = "USA,UK" };

            bool isValid = preferences.IsValid;

            Assert.True(isValid);
        }

        [Fact]
        public void IsValid_DecadeGreaterThanZero_ReturnsTrue()
        {
            UserPreferences preferences = new UserPreferences { Decade = 2010 };

            bool isValid = preferences.IsValid;

            Assert.True(isValid);
        }

        [Fact]
        public void IsValid_DecadeIsZero_ReturnsFalse()
        {
            UserPreferences preferences = new UserPreferences { Decade = 0 };

            bool isValid = preferences.IsValid;

            Assert.False(isValid);
        }

        [Fact]
        public void IsValid_AgeSet_ReturnsFalse()
        {
            UserPreferences preferences = new UserPreferences { Age = "16+" };

            bool isValid = preferences.IsValid;

            Assert.False(isValid);
        }

        [Fact]
        public void UpdateFrom_UpdatesAllProperties()
        {
            UserPreferences originalPreferences = new UserPreferences
            {
                Genres = "Action",
                Countries = "USA",
                Decade = 2000,
                Age = "16+"
            };

            UserPreferences newPreferences = new UserPreferences
            {
                Genres = "Comedy,Romance",
                Countries = "UK,Canada",
                Decade = 2010,
                Age = "18+"
            };

            originalPreferences.UpdateFrom(newPreferences);

            Assert.Equal("Comedy,Romance", originalPreferences.Genres);
            Assert.Equal("UK,Canada", originalPreferences.Countries);
            Assert.Equal(2010, originalPreferences.Decade);
            Assert.Equal("18+", originalPreferences.Age);
        }
    }
}
