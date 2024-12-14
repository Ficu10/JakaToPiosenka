using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using JakaToPiosenka.HelpClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JakaToPiosenka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AfterGameKalambury : ContentPage
    {
        private readonly Sounds _sounds = new Sounds();
        private int listScore = 0;

        public AfterGameKalambury()
        {
            InitializeComponent();
            Result.Text = listScore.ToString();

            if (MultiplayerPage.isMultiplayerEnabled)
            {
                MultiplayerButton.IsVisible = true;
                NextPlayer.IsVisible = true;
                AddGamesAndPoints(BeforeGameKalambury.SortedPlayers);
            }
            else
            {
                MultiplayerButton.IsVisible = false;
                NextPlayer.IsVisible = false;
                PlayerName.Text = "";
            }

            // Play sound based on score
            PlayScoreSound();

            // Animate the songs
            _ = AnimateSongsAsync();

            // Reset game state
            ResetGameState();
        }



        public void AddGamesAndPoints(ObservableCollection<Multiplayer> sortedPlayers)
        {
            // Pobierz i posortuj graczy
            var players = Multiplayer.GetAllPlayers()
                .OrderBy(p => p.GamesNumber) // Najpierw według liczby gier (malejąco)
                .ThenBy(p => p.Name) // Potem alfabetycznie
                .ToList();

            // Wyczyść istniejącą kolekcję (jeśli istnieje)
            sortedPlayers.Clear();

            // Wczytaj graczy do ObservableCollection
            foreach (var player in players)
            {
                sortedPlayers.Add(player);
            }

            // Jeśli lista graczy nie jest pusta, aktualizuj dane pierwszego gracza
            if (players.Any())
            {
                var firstPlayer = players.First();

                // Zwiększ liczbę gier i punkty
                firstPlayer.GamesNumber = firstPlayer.GamesNumber + 1;
                firstPlayer.Points += Game.pointsCounter;
                PlayerName.Text = firstPlayer.Name;

                var secondplayer = players[1]; // Drugi gracz w kolejności
                NextPlayer.Text += "Następny gracz: " + secondplayer.Name;

                // Zapisz zmiany w bazie danych
                Multiplayer.UpdatePlayer(firstPlayer);
            }
        }


        /// <summary>
        /// Odtwarza dźwięk w zależności od wyniku gracza.
        /// </summary>
        private void PlayScoreSound()
        {
            double score = (Convert.ToDouble(Game.pointsCounter) / Convert.ToDouble(SettingsPage.WordsNumber));
            if (score <= 0.3)
            {
                _sounds.BadScoreSound();
            }
            else if (score <= 0.7) // 4-6
            {
                _sounds.MediumScoreSound();
            }
            else
            {
                _sounds.GoodScoreSound();
            }
        }

        private List<SongItem> PrepareSongsWithColors()
        {
            var songsWithColors = new List<SongItem>();

            for (int i = 0; i < SettingsPage.WordsNumber; i++)
            {
                if (Game.goodBadSongs[i] == 1) // Correct answer
                {
                    songsWithColors.Add(new SongItem
                    {
                        Title = Game.songsFromGame[i],
                        StartColor = Color.FromHex("#d19d49"), // Light green at the top-left
                        MiddleColor = Color.FromHex("#64b840"), // Dark green in the center
                        EndColor = Color.FromHex("#d19d49") // Light green at the bottom-right
                    });

                }
                else // Incorrect answer
                {
                    songsWithColors.Add(new SongItem
                    {
                        Title = Game.songsFromGame[i],
                        StartColor = Color.FromHex("#d19d49"), // Light red at the top-left
                        MiddleColor = Color.FromHex("#a63430"), // Dark red in the center
                        EndColor = Color.FromHex("#d19d49")    // Light red at the bottom-right
                    });
                }
            }

            return songsWithColors;
        }




        /// <summary>
        /// Resetuje stan gry po zakończeniu rundy.
        /// </summary>
        private void ResetGameState()
        {
            Game.pointsCounter = 0;
            MessagingCenter.Send(new OrientationMessage { IsLandscape = false }, "SetOrientation");
        }

        /// <summary>
        /// Obsługa przycisku "Restart Gry".
        /// </summary>
        private async void RestartGame_Clicked(object sender, EventArgs e)
        {
            _sounds.ClickSound();
            await Navigation.PushAsync(new RulesPage());
        }

        /// <summary>
        /// Obsługa przycisku "Menu".
        /// </summary>
        private async void Menu_Clicked(object sender, EventArgs e)
        {
            _sounds.ClickSound();
            await Navigation.PushAsync(new KalamburyPage());
        }

        /// <summary>
        /// Zablokowanie przycisku "Wstecz".
        /// </summary>
        protected override bool OnBackButtonPressed()
        {
            return true; // Zablokowanie przycisku
        }

        private async void Multiplayer_Clicked(object sender, EventArgs e)
        {
            _sounds.ClickSound();
            await Navigation.PushAsync(new RankingPage());
        }



        /// <summary>
        /// Animates the sequential reveal of songs in the ListView, with scrolling starting from the third item.
        /// </summary>
        /// <returns></returns>
        private async Task AnimateSongsAsync()
        {
            // Clear the ListView initially
            myListView.ItemsSource = null;

            var songsWithColors = PrepareSongsWithColors();
            var displayedSongs = new ObservableCollection<SongItem>();

            myListView.ItemsSource = displayedSongs;

            // Populate the ListView with songs
            foreach (var song in songsWithColors)
            {
                displayedSongs.Add(song);
            }

            // Initial setup: make all items covered (invisible)
            for (int i = 0; i < songsWithColors.Count; i++)
            {
                var viewCell = myListView.TemplatedItems[i] as ViewCell;
                if (viewCell?.View != null)
                {
                    var cellView = viewCell.View;
                    cellView.Opacity = 0; // Start fully transparent
                    cellView.Scale = 0.8; // Start slightly smaller
                }
            }

            // Sequentially uncover items with animations
            // Sequentially uncover items with animations
            for (int i = 0; i < songsWithColors.Count; i++)
            {
                var viewCell = myListView.TemplatedItems[i] as ViewCell;
                if (viewCell?.View != null)
                {
                    if (i > 1) // Third item (zero-based index)
                    {
                        myListView.ScrollTo(displayedSongs[i], ScrollToPosition.Center, true);
                    }
                    await Task.Delay(200); // Adjust for pacing

                    var cellView = viewCell.View;

                    // Animate the opacity and scale for reveal effect

                    await cellView.FadeTo(1, 100, Easing.CubicInOut); // Fade in
                    if (Game.goodBadSongs[i] == 1) // Correct answer
                    {
                        CurrentScore++; // Increment score

                        // Update the result text dynamically
                        Result.Text = CurrentScore.ToString();


                        // Update the gradient dynamically
                        UpdateBackgroundGradient();

                    }

                    // Animate the cell view for reveal effect
                    await cellView.ScaleTo(1.4, 300, Easing.CubicOut);
                    await cellView.ScaleTo(1.0, 300, Easing.CubicIn); // Return to normal size


                }

                // Delay before revealing the next item
                await Task.Delay(450); // Adjust for pacing
            }

        }


        public int CurrentScore
        {
            get => listScore;
            set
            {
                listScore = value;
                OnPropertyChanged(nameof(CurrentScore)); // Notify the UI of the change
                UpdateBackgroundGradient();
            }
        }

        private void UpdateBackgroundGradient()
        {
            double scoreFraction = Math.Min(CurrentScore / (double)SettingsPage.WordsNumber, 1.0); // Normalize between 0 and 1

            Color startColor, middleColor, endColor;

            if (scoreFraction <= 0.5) // Extended Bronze Phase (0 to 50%)
            {
                double localFraction = scoreFraction / 0.5; // Normalize to range [0, 1] for bronze
                startColor = InterpolateColors(Color.FromHex("#D2A679"), Color.FromHex("#E6E6E6"), localFraction); // Bronze to light silver
                middleColor = InterpolateColors(Color.FromHex("#A0522D"), Color.FromHex("#C0C0C0"), localFraction); // Dark bronze to silver
                endColor = InterpolateColors(Color.FromHex("#8B4513"), Color.FromHex("#808080"), localFraction); // Darkest bronze to dark silver
            }
            else if (scoreFraction <= 0.9) // Extended Silver Phase (50% to 90%)
            {
                double localFraction = (scoreFraction - 0.5) / 0.4; // Normalize to range [0, 1] for silver
                startColor = InterpolateColors(Color.FromHex("#E6E6E6"), Color.FromHex("#FFFACD"), localFraction); // Light silver to light gold
                middleColor = InterpolateColors(Color.FromHex("#C0C0C0"), Color.FromHex("#FFD700"), localFraction); // Silver to gold
                endColor = InterpolateColors(Color.FromHex("#808080"), Color.FromHex("#B8860B"), localFraction); // Dark silver to dark gold
            }
            else // Final Gold Phase (90% to 100%)
            {
                double localFraction = (scoreFraction - 0.9) / 0.1; // Normalize to range [0, 1] for gold
                startColor = InterpolateColors(Color.FromHex("#FFFACD"), Color.FromHex("#FFFACD"), localFraction); // Light gold remains
                middleColor = InterpolateColors(Color.FromHex("#FFD700"), Color.FromHex("#FFD700"), localFraction); // True gold remains
                endColor = InterpolateColors(Color.FromHex("#B8860B"), Color.FromHex("#B8860B"), localFraction); // Dark gold remains
            }


            // Apply the updated gradient to the background
            ScoreBackground.Background = new RadialGradientBrush
            {
                Center = new Point(0.5f, 0.5f),
                Radius = 0.5f,
                GradientStops =
        {
            new GradientStop { Color = startColor, Offset = 0.0f },  // Lighter center
            new GradientStop { Color = middleColor, Offset = 0.5f }, // Stronger middle
            new GradientStop { Color = endColor, Offset = 1.0f }     // Darker edge
        }
            };
        }

        private Color InterpolateColors(Color from, Color to, double fraction)
        {
            // Ensure fraction is clamped to [0, 1]
            fraction = fraction < 0.0 ? 0.0 : fraction > 1.0 ? 1.0 : fraction;

            // Convert normalized color values to [0, 255], interpolate, and return a new Color
            return Color.FromRgb(
                (int)((from.R * 255) + ((to.R * 255) - (from.R * 255)) * fraction),
                (int)((from.G * 255) + ((to.G * 255) - (from.G * 255)) * fraction),
                (int)((from.B * 255) + ((to.B * 255) - (from.B * 255)) * fraction)
            );
        }




    }

    /// <summary>
    /// Klasa reprezentująca dane dla ListView.
    /// </summary>
    public class SongItem
    {
        public string Title { get; set; }
        public Color StartColor { get; set; } // Lighter color at the edges
        public Color MiddleColor { get; set; } // Darker color in the middle
        public Color EndColor { get; set; } // Lighter color at the edges
    }


}