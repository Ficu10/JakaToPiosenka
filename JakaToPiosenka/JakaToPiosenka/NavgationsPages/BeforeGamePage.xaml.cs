using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JakaToPiosenka.HelpClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace JakaToPiosenka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BeforeGamePage : ContentPage
    {
        public static int timeChanger;
        Sounds sound = new Sounds();
        public BeforeGamePage()
        {
            MessagingCenter.Send(new OrientationMessage { IsLandscape = false }, "SetOrientation");

            InitializeComponent();
            if (MultiplayerPage.isMultiplayerEnabled)
            {
                MultiplayerButton.IsVisible = true;
            }
            else
            {
                MultiplayerButton.IsVisible = false;
            }
            SettingsPage.Time1 = SettingsHelper.GetValue("Time1", 15);
            SettingsPage.Time2 = SettingsHelper.GetValue("Time2", 30);
            SettingsPage.Time3 = SettingsHelper.GetValue("Time3", 45);
            SettingsPage.Time4 = SettingsHelper.GetValue("Time4", 60);
            SettingsPage.WordsNumber = SettingsHelper.GetValue("WordsNumber", 10);

            Time15.Text = SettingsPage.Time1.ToString();
            Time30.Text = SettingsPage.Time2.ToString();
            Time45.Text = SettingsPage.Time3.ToString();
            Time60.Text = SettingsPage.Time4.ToString();

            Dictionary<string, (string, string)> gameModeMappings = new Dictionary<string, (string, string)>
            {
                { "AllSongs", ("Wszystkie gatunki", "Songs.jpg") },
                { "FairyTales", ("Piosenki z bajek", "DSongs.jpg") },
                { "Pop", ("Pop", "Pop.jpg") },
                { "Rock", ("Rock", "Rock.jpg") },
                { "UsersMusic", ("Twoja muzyka", "AIMusic.jpg") },
                { "Rap", ("Rap", "Rap.jpg") },
                { "RapPolish", ("Rap Polski", "PRap.jpg") },
                { "RapEnglish", ("Rap Zagraniczny", "IRap.jpg") },
                { "PopPolish", ("Pop Polski", "PPop.jpg") },
                { "PopEnglish", ("Pop Zagraniczny", "IPop.jpg") },
                { "The80", ("Lata 80'", "a80s.jpg") },
                { "The80Polish", ("Polskie lata 80'", "a80sPL.jpg") },
                { "The80English", ("Zagraniczne lata 80'", "a80I.jpg") },
                { "RockPolish", ("Rock Polski", "PRock.jpg") },
                { "Carols", ("Kolędy", "Carols.jpg") },
                { "ChristmasSongs", ("Świąteczne Piosenki", "ICarols.jpg") },
                { "RockEnglish", ("Rock Zagraniczny", "IRock.jpg") },
                { "Youtube", ("Hity Youtube", "YTMusic.jpg") },

            };

            if (gameModeMappings.TryGetValue(MainPage.gameMode, out var mappings))
            {
                Category.Text = mappings.Item1;

                PhotoCategory.Source = mappings.Item2;
            }

            if (MultiplayerPage.isMultiplayerEnabled)
            {
                MultiplayerButton.IsVisible = true;
                PlayerName.IsVisible = true;

                // Załaduj i wyświetl graczy
                LoadAndDisplayPlayers(BeforeGameKalambury.SortedPlayers, PlayerName);
            }
            else
            {
                MultiplayerButton.IsVisible = false;
                PlayerName.IsVisible = false;
            }
        }

        public void LoadAndDisplayPlayers(ObservableCollection<Multiplayer> sortedPlayers, Label playerNameLabel, SelectionChangedEventArgs e = null)
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

            // Jeśli lista graczy nie jest pusta, ustaw pierwszy element jako domyślny
            if (players.Any())
            {
                var firstPlayer = players.First();
                playerNameLabel.Text = $"Gracz: {firstPlayer.Name}";
            }
            else
            {
                playerNameLabel.Text = "Brak graczy do wyświetlenia.";
            }

            // Obsłuż wybór gracza, jeśli istnieje zdarzenie SelectionChanged
            if (e?.CurrentSelection.FirstOrDefault() is Multiplayer selectedPlayer)
            {
                playerNameLabel.Text = $"Gracz: {selectedPlayer.Name}";
            }
        }
        async void Time15_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            timeChanger = SettingsPage.Time1;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time30_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            timeChanger = SettingsPage.Time2;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time45_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            timeChanger = SettingsPage.Time3;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time60_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            timeChanger = SettingsPage.Time4;
            await Navigation.PushAsync(new RulesPage());
        }
        private async void AddNewSongs_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            await Navigation.PushAsync(new AddingNewSongs());
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

        private async void Multiplayer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RankingPage());
        }
    }
}