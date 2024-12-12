﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using JakaToPiosenka.HelpClasses;

namespace JakaToPiosenka
{
    public partial class MultiplayerPage : ContentPage
    {
        private readonly Sounds _sounds = new Sounds();

        public ObservableCollection<Multiplayer> Players { get; set; } = new ObservableCollection<Multiplayer>();
        int numPlayers = 0;
        public static bool isMultiplayerEnabled = SettingsHelper.Get("MultiplayerEnabled", "false") == "true";

        public MultiplayerPage()
        {
            InitializeComponent();

            // Upewnij się, że baza danych jest zainicjalizowana
            Multiplayer.InitializeDatabase();

            // Pobierz graczy z bazy danych
            var playersFromDb = Multiplayer.GetAllPlayers();
            foreach (var player in playersFromDb)
            {
                Players.Add(player);
            }
            numPlayers = Players.Count;

            // Ustaw przełącznik na podstawie wartości w bazie danych
            MultiplayerSwitch.IsToggled = SettingsHelper.Get("MultiplayerEnabled", "false") == "true";

            BindingContext = this;
            NumberOfPlayer.Text = numPlayers.ToString();
        }
        private void PlayerNameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text.Length > 10)
            {
                // Przycięcie tekstu do maksymalnej długości
                entry.Text = entry.Text.Substring(0, 10);
            }
        }

        private async void OnAddPlayerClicked(object sender, EventArgs e)
        {
            _sounds.ClickSound();
            if (!string.IsNullOrWhiteSpace(PlayerNameEntry.Text))
            {
                if (Players.Any(p => p.Name == PlayerNameEntry.Text))
                {
                    await DisplayAlert("Błąd", "Podana nazwa jest zajęta.", "OK");
                }
                else
                {
                    var newPlayer = new Multiplayer { Name = PlayerNameEntry.Text, Points = 0 };

                    // Dodaj gracza do listy i bazy danych
                    Players.Add(newPlayer);
                    Multiplayer.AddPlayerToDatabase(newPlayer);

                    numPlayers++;
                    PlayerNameEntry.Text = string.Empty;
                    NumberOfPlayer.Text = numPlayers.ToString();
                }
            }
            else
            {
                await DisplayAlert("Błąd", "Wpisz nazwę gracza.", "OK");
            }
        }

        private async void OnRemovePlayerClicked(object sender, EventArgs e)
        {
            _sounds.DeleteSound();
            if (sender is Button button && button.BindingContext is Multiplayer player)
            {
                // Usuń gracza z listy i bazy danych
                Players.Remove(player);
                Multiplayer.RemovePlayerFromDatabase(player);

                numPlayers--;
                NumberOfPlayer.Text = numPlayers.ToString();

                if (numPlayers < 2 && MultiplayerSwitch.IsToggled)
                {
                    await DisplayAlert("Błąd", "Potrzebujesz przynajmniej dwóch graczy", "OK");
                    MultiplayerSwitch.IsToggled = false;
                }
            }
        }

        private void OnMultiplayerSwitchToggled(object sender, ToggledEventArgs e)
        {
            _sounds.Toggle();
            if (e.Value) // Włączony tryb multiplayer
            {
                if (Players.Count < 2)
                {
                    MultiplayerSwitch.IsToggled = false;
                    return;
                }

                SettingsHelper.Set("MultiplayerEnabled", "true");
                isMultiplayerEnabled = true;
            }
            else // Wyłączony tryb multiplayer
            {
                SettingsHelper.Set("MultiplayerEnabled", "false");
                isMultiplayerEnabled = false;
            }
        }
        protected override bool OnBackButtonPressed()
        {
            if (MainPage.isMainPage)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new MainPage());
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new KalamburyPage());
                });
            }

            return true;
        }
    }
}