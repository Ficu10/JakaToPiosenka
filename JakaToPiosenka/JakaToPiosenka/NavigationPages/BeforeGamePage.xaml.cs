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

          Time15.Text = SettingsPage.Time1.ToString();
          Time30.Text = SettingsPage.Time2.ToString();
          Time45.Text = SettingsPage.Time3.ToString();
          Time60.Text = SettingsPage.Time4.ToString();

            Dictionary<string, (string, string)> gameModeMappings = new Dictionary<string, (string, string)>
            {
                { "AllSongs", ("Wszystkie gatunki", "WszystkiePiosenki.jpg") },
                { "FairyTales", ("Piosenki z bajek", "PiosenkiZBajek.jpg") },
                { "Pop", ("Pop", "pop.jpg") },
                { "Rock", ("Rock", "rock.jpg") },
                { "UsersMusic", ("Twoja muzyka", "yourMusic.jpg") },
                { "Rap", ("Rap", "rap.jpg") },
                { "RapPolish", ("Rap Polski", "RapPolski.jpg") },
                { "RapEnglish", ("Rap Zagraniczny", "RapZagraniczny.jpg") },
                { "PopPolish", ("Pop Polski", "PopPolski.jpg") },
                { "PopEnglish", ("Pop Zagraniczny", "PopZagraniczny.jpg") },
                { "The80", ("Lata 80'", "Lata80.jpg") },
                { "The80Polish", ("Polskie lata 80'", "PolskieLata80.jpg") },
                { "The80English", ("Zagraniczne lata 80'", "ZagraniczneLata80.jpg") },
                { "RockPolish", ("Rock Polski", "RockPolski.jpg") },
                { "Carols", ("Kolędy", "RockZagraniczny.jpg") },
                { "ChristmasSongs", ("Świąteczne Piosenki", "RockZagraniczny.jpg") },
               
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