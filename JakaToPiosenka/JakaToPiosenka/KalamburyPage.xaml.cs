using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms.Shapes;
using SQLite;
using System.Reflection;
using System.Runtime.InteropServices;
using JakaToPiosenka.MusicClasses;

namespace JakaToPiosenka
{
    public partial class KalamburyPage : ContentPage
    {
        public static bool orientationPortrait = true;
        public static string gameMode = "allSongs";

        private bool isScrollLocked = false; // Flag to indicate if scrolling is locked
        private double maxScrollPosition = 950; // Maximum scroll position

        Sounds sound = new Sounds();
        public KalamburyPage()
        {
            MessagingCenter.Send(new OrientationMessage { IsLandscape = false }, "SetOrientation");
            InitializeComponent();
            var musicTypesSongs = new List<MUSICTYPES>()

            {
                new Pop(),
                new Rock(),
                new FairyTales(),
                new AllSongs(),
                new Rap(),
                new UsersMusic(),
                new The80(),
                new The80English(),
                new The80Polish(),
                new RapEnglish(),
                new RapPolish(),
                new PopEnglish(),
                new PopPolish(),
                new RockEnglish(),
                new RockPolish(),
                new Youtube()

            };
            bool isTableCreated = MUSICTYPES.connection.GetTableInfo("Pop").Any();
            if (!isTableCreated)
            {
                foreach (var item in musicTypesSongs)
                {
                    item.Load();
                }
            }

        }

        private async void Multiplayer_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MultiplayerPage());


        }

        private async void Settings_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());


        }
        private async void OnMenuButtonClicked(object sender, EventArgs e)
        {
            MenuView.IsVisible = true;
            LeftMenu.IsVisible = false;
            await MenuView.TranslateTo(0, 0, 500, Easing.CubicOut);

        }

        private async void OnCloseMenuClicked(object sender, EventArgs e)
        {
            await MenuView.TranslateTo(-200, 0, 500, Easing.CubicIn);
            MenuView.IsVisible = false;
            LeftMenu.IsVisible = true;

        }
        async void AllSongsButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "AllSongs";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        async void RockButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "Rock";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        async void PopButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "Pop";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        async void DisneyButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "FairyTales";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        async void UsersMusicButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "UsersMusic";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        async void RapButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "Rap";
            await Navigation.PushAsync(new BeforeGamePage());
        }
        protected override bool OnBackButtonPressed()
        {

            return true;
        }

        private async void The80Button_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "The80";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private async void The80PolishButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "The80Polish";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private async void RockPolishButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "RockPolish";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private async void RockEnglishButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "RockEnglish";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private async void RapPolishButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "RapPolish";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private async void RapEnglishButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "RapEnglish";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private async void PopPolishButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "PopPolish";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private async void PopEnglishButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "PopEnglish";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private async void The80English_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "The80English";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private void Scroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (isScrollLocked)
            {
                if (Scroll.ScrollY > maxScrollPosition)
                {
                    // Scroll back to the maximum allowed position
                    Scroll.ScrollToAsync(Scroll.ScrollX, maxScrollPosition, false);
                }
            }
            else
            {
                if (Scroll.ScrollY > maxScrollPosition)
                {
                    // Lock scrolling beyond the maximum position
                    isScrollLocked = true;
                    Scroll.ScrollToAsync(Scroll.ScrollX, maxScrollPosition, false);
                }
            }
        }

        private async void Youtube_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "Youtube";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private async void NoteButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            await Navigation.PushAsync(new MainPage());
        }

        private async void KalamburyButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            await Navigation.PushAsync(new KalamburyPage());
        }
    }
}
