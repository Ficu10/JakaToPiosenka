﻿using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace JakaToPiosenka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BeforeGamePage : ContentPage
    {
        public static int timeChanger = 30;
        Sounds sound = new Sounds();    
        public BeforeGamePage()
        {
            MessagingCenter.Send(new OrientationMessage { IsLandscape = false }, "SetOrientation");

            InitializeComponent();

            if (MainPage.isMainPage)
            {
                BackgroundColor = Color.FromHex("#87b3d1");
                Time15.BackgroundColor = Color.FromHex("#1a7cad");
                Time30.BackgroundColor = Color.FromHex("#1a7cad");
                Time45.BackgroundColor = Color.FromHex("#1a7cad");
                Time60.BackgroundColor = Color.FromHex("#1a7cad");
                AddNewSongs.BackgroundColor = Color.FromHex("#0478b3");
                AddNewSongs.Text = "Dodaj piosenki";
            }
            else
            {
                BackgroundColor = Color.FromHex("#dba348");
                Time15.BackgroundColor = Color.FromHex("#e08c04");
                Time30.BackgroundColor = Color.FromHex("#e08c04");
                Time45.BackgroundColor = Color.FromHex("#e08c04");
                Time60.BackgroundColor = Color.FromHex("#e08c04");
                AddNewSongs.BackgroundColor = Color.FromHex("#fc9d03");
                AddNewSongs.Text = "Dodaj hasłą";

            }

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
                { "RockEnglish", ("Rock Zagraniczny", "RockZagraniczny.jpg") },
                { "Children", ("Dla Dzieci", "dladzieci1.jpg") },
                { "Countries", ("Państwa", "panstwa.jpg") },
                { "Emotions", ("Emocje", "emocje.jpg") },
                { "FictionalCharacter", ("Postacie Fikcyjne", "postacfikcyjna.jpg") },
                { "HistoricalCharcter", ("Postacie Historycze", "mini4.jpg") },
                { "Jobs", ("Zawody", "gornik.jpg") },
                { "Movies", ("Filmy", "filmy.jpg") },
                { "Series", ("Seriale", "netflix.png") },
                { "Tales", ("Bajki", "bajki.jpg") },
                { "Words", ("Przysłowia", "przyslowia.jpg") },
            };

            if (gameModeMappings.TryGetValue(MainPage.gameMode, out var mappings))
            {
                Category.Text = mappings.Item1;
                
                PhotoCategory.Source = mappings.Item2;
            }
        }
        async void Time15_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            timeChanger = 15;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time30_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            timeChanger = 30;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time45_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            timeChanger = 45;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time60_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            timeChanger = 60;
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
    }
}