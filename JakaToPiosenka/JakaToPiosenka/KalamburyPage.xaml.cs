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
using JakaToPiosenka;

namespace JakaToPiosenka
{
    public partial class KalamburyPage : ContentPage
    {
        public static bool orientationPortrait = true;
        public static string gameMode = "allSongs";

        private bool isScrollLocked = false; // Flag to indicate if scrolling is locked
        private double maxScrollPosition = 550; // Maximum scroll position

        Sounds sound = new Sounds();
        public KalamburyPage()
        {
            MessagingCenter.Send(new OrientationMessage { IsLandscape = false }, "SetOrientation");
            InitializeComponent();
          

        }

        private async void Multiplayer_Click(object sender, EventArgs e)
        {
            sound.ClickSound();
            await Navigation.PushAsync(new MultiplayerPage());


        }

        private async void Settings_Click(object sender, EventArgs e)
        {
            sound.ClickSound();
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
        
        protected override bool OnBackButtonPressed()
        {

            return true;
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
        private async void przyslowiaButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Words";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void panstwaButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Countries";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void netflixButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Series";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void bajkiButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Tales";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void filmyButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Movies";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void emocjeButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Emotions";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void dladzieciButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Children";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void postaciehistoryczne_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "HistoricalCharacter";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void zawodyButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Jobs";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void fictionButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "FictionalCharacter";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }
    }
}
