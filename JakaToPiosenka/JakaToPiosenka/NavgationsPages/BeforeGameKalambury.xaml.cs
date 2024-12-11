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
    public partial class BeforeGameKalambury : ContentPage
    {
        public static ObservableCollection<Multiplayer> SortedPlayers { get; set; } = new ObservableCollection<Multiplayer>();

        Sounds sound = new Sounds();
        public BeforeGameKalambury()
        {
            MessagingCenter.Send(new OrientationMessage { IsLandscape = false }, "SetOrientation");


            InitializeComponent();

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

                { "Children", ("Dla Dzieci", "CKal.jpg") },
                { "Countries", ("Państwa", "World.jpg") },
                { "Emotions", ("Emocje", "Emotes.jpg") },
                { "FictionalCharacter", ("Postacie Fikcyjne", "Fiction.jpg") },
                { "HistoricalCharacter", ("Postacie Historycze", "History.jpg") },
                { "Jobs", ("Zawody", "Jobs.jpg") },
                { "Movies", ("Filmy", "Movies.jpg") },
                { "Series", ("Seriale", "Series.jpg") },
                { "Tales", ("Bajki", "Fairy.jpg") },
                { "Words", ("Przysłowia", "Proverb.jpg") },
                { "AdultMixed", ("+18", "Art18.jpg") },
                { "Animals", ("Zwierzęta", "animals.jpg") },
                { "Celebrities", ("Celebryci", "Celebs.jpg") },
                { "DailyLife", ("Codzienne Życie", "daily.jpg") },
                { "Poland", ("Polska", "Poland.jpg") },
                { "Rhymes", ("Rymy", "Mickiew.jpg") },
                { "ScienceTopics", ("Nauka", "Science.jpg") },
                { "Sports", ("Sport", "Sports.jpg") },
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
            BeforeGamePage.timeChanger = SettingsPage.Time1;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time30_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            BeforeGamePage.timeChanger = SettingsPage.Time2;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time45_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            BeforeGamePage.timeChanger = SettingsPage.Time3;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time60_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            BeforeGamePage.timeChanger = SettingsPage.Time4;
            await Navigation.PushAsync(new RulesPage());
        }

        private async void AddNewSongs_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            await Navigation.PushAsync(new AddingNewWords());
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