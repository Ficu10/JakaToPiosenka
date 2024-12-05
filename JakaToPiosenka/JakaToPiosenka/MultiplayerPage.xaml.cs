using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using Xamarin.Forms;

namespace JakaToPiosenka
{
    public partial class MultiplayerPage : ContentPage
    {
        // ObservableCollection for binding player data
        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();
        int numPlayers = 0;
        public MultiplayerPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void OnAddPlayerClicked(object sender, EventArgs e)
        {
            bool ifExist = false;
            if (!string.IsNullOrWhiteSpace(PlayerNameEntry.Text))
            {
                for (int i = 0; i < numPlayers; i++)
                {
                    if (PlayerNameEntry.Text == Players[i].Name)
                    {
                        ifExist = true;
                    }
                    else
                    {
                        ifExist = false;
                    }

                }
                if (ifExist)
                {
                    await DisplayAlert("Błąd", "Podana nazwa jest zajęta.", "OK");
                }
                else
                {
                    // Add the player to the list
                    numPlayers++;
                    Players.Add(new Player { Name = PlayerNameEntry.Text });
                    PlayerNameEntry.Text = string.Empty; // Clear the input field
                    NumberOfPlayer.Text = numPlayers.ToString();
                }
                
            }
            else
            {
                await DisplayAlert("Błąd", "Wpisz nazwę gracza.", "OK");
            }
            ifExist = false;
        }

        private async void OnRemovePlayerClicked(object sender, EventArgs e)
        {
            // Get the button and its bound Player object
            if (sender is Button button && button.BindingContext is Player player)
            {
                Players.Remove(player);
                numPlayers--;
                NumberOfPlayer.Text = numPlayers.ToString();
                if (numPlayers < 2 && MultiplayerSwitch.IsToggled == true)
                {
                    await DisplayAlert("Błąd", "Potrzebujesz przynajmniej dwóch graczy", "OK");
                    MultiplayerSwitch.IsToggled = false; // Wyłącz przełącznik
                }
            }
        }

        private async void OnMultiplayerSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value) // Jeśli przełącznik jest włączony
            {
                if (Players.Count < 2)
                {
                    await DisplayAlert("Błąd", "Potrzebujesz przynajmniej dwóch graczy", "OK");
                    MultiplayerSwitch.IsToggled = false; // Wyłącz przełącznik
                    return;
                }

                await DisplayAlert("Tryb Multiplayer aktywowany", $"Włączono tryb Multiplayer.", "OK");
            }
            else // Jeśli przełącznik jest wyłączony
            {
                await DisplayAlert("Tryb Multiplayer dezaktywowany", "Wyłączono tryb Multiplayer.", "OK");
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

    // Player model
    public class Player
    {
        public string Name { get; set; }
    }
}
