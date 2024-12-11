﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

            // Ustawienie dźwięku w zależności od wyniku
            PlayScoreSound();



            // Przygotowanie danych dla ListView z kolorami komórek
            var songsWithColors = PrepareSongsWithColors();

            // Przypisanie danych do ListView
            myListView.ItemsSource = songsWithColors;

            // Resetowanie licznika punktów
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

        private async void Multiplayer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RankingPage());
        }
    }


}