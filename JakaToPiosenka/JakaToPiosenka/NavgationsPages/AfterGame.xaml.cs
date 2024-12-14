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
    public partial class AfterGame : ContentPage
    {
        private readonly Sounds _sounds = new Sounds();

        public AfterGame()
        {
            InitializeComponent();

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
                Resut.Text = $"Twój wynik to {Game.pointsCounter}/{SettingsPage.WordsNumber}";
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
                Resut.Text = "Wynik gracza " + firstPlayer.Name + ": " + Game.pointsCounter + "/" + SettingsPage.WordsNumber;

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

        /// <summary>
        /// Przygotowuje dane do ListView z odpowiednimi kolorami komórek.
        /// </summary>
        /// <returns>Lista obiektów SongItem z tytułami i kolorami.</returns>
        private List<SongItem> PrepareSongsWithColors()
        {
            var songsWithColors = new List<SongItem>();

            for (int i = 0; i < SettingsPage.WordsNumber; i++)
            {
                songsWithColors.Add(new SongItem
                {
                    Title = Game.songsFromGame[i], // Zakładam, że `songsFromGame` to lista stringów
                    CellColor = Game.goodBadSongs[i] == 1 ? Color.FromHex("#7cba61") : Color.FromHex("#ab524f")
                });
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
            await Navigation.PushAsync(new MainPage());
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
                    await cellView.ScaleTo(1.4, 300, Easing.CubicOut); // Scale up
                    await cellView.ScaleTo(1.0, 300, Easing.CubicIn); // Return to normal size
                }

                // Delay before revealing the next item
                await Task.Delay(450); // Adjust for pacing
            }

        }




    }

    /// <summary>
    /// Klasa reprezentująca dane dla ListView.
    /// </summary>
    public class SongItem
    {
        public string Title { get; set; } // Tytuł piosenki
        public Color CellColor { get; set; } // Kolor komórki
    }
}