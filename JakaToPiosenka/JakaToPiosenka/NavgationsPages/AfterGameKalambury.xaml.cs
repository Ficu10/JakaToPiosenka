using System;
using System.Collections.Generic;
using JakaToPiosenka.HelpClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JakaToPiosenka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AfterGameKalambury : ContentPage
    {
        private readonly Sounds _sounds = new Sounds();

        public AfterGameKalambury()
        {
            InitializeComponent();

            // Ustawienie dźwięku w zależności od wyniku
            PlayScoreSound();

            // Wyświetlenie wyniku
            Resut.Text = $"Twój wynik to {Game.pointsCounter}/10";

            // Przygotowanie danych dla ListView z kolorami komórek
            var songsWithColors = PrepareSongsWithColors();

            // Przypisanie danych do ListView
            myListView.ItemsSource = songsWithColors;

            // Resetowanie licznika punktów
            ResetGameState();
        }

        /// <summary>
        /// Odtwarza dźwięk w zależności od wyniku gracza.
        /// </summary>
        private void PlayScoreSound()
        {
            if (Game.pointsCounter <= 3)
            {
                _sounds.BadScoreSound();
            }
            else if (Game.pointsCounter <= 6) // 4-6
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

            for (int i = 0; i < Game.songsFromGame.Count; i++)
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
            await Navigation.PushAsync(new RulesPage());
        }

        /// <summary>
        /// Obsługa przycisku "Menu".
        /// </summary>
        private async void Menu_Clicked(object sender, EventArgs e)
        {
            if (MainPage.isMainPage)
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await Navigation.PushAsync(new KalamburyPage());
            }
        }

        /// <summary>
        /// Zablokowanie przycisku "Wstecz".
        /// </summary>
        protected override bool OnBackButtonPressed()
        {
            return true; // Zablokowanie przycisku
        }
    }

    
}
