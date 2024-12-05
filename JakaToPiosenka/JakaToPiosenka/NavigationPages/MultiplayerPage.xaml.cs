﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace JakaToPiosenka
{
    public partial class MultiplayerPage : ContentPage
    {
        public ObservableCollection<Multiplayer> Players { get; set; } = new ObservableCollection<Multiplayer>();
        int numPlayers = 0;
        public static bool isMultiplayerEnabled = Settings.Get("MultiplayerEnabled", "false") == "true";


        public MultiplayerPage()
        {
            InitializeComponent();
            Multiplayer.InitializeDatabase();

            // Pobierz graczy z bazy danych
            var playersFromDb = Multiplayer.GetAllPlayers();
            foreach (var player in playersFromDb)
            {
                Players.Add(player);
            }
            numPlayers = Players.Count;

            // Ustaw przełącznik na podstawie wartości w bazie danych
            var multiplayerEnabled = Settings.Get("MultiplayerEnabled", "false") == "true";
            MultiplayerSwitch.IsToggled = multiplayerEnabled;

            BindingContext = this;
            NumberOfPlayer.Text = numPlayers.ToString();
        }

        private async void OnAddPlayerClicked(object sender, EventArgs e)
        {
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

        private  void OnMultiplayerSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value) // Włączony tryb multiplayer
            {
                if (Players.Count < 2)
                {
                    MultiplayerSwitch.IsToggled = false;
                    return;
                }

                Settings.Set("MultiplayerEnabled", "true");
                isMultiplayerEnabled = true;
            }
            else // Wyłączony tryb multiplayer
            {
                Settings.Set("MultiplayerEnabled", "false");
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