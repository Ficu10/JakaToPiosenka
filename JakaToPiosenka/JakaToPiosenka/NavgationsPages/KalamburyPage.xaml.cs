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
using JakaToPiosenka.HelpClasses;

namespace JakaToPiosenka
{
    public partial class KalamburyPage : ContentPage
    {
        public static bool orientationPortrait = true;
        public static string gameMode = "allSongs";

        private bool isScrollLocked = false; // Flag to indicate if scrolling is locked
        private double maxScrollPosition = 1200; // Maximum scroll position

        Sounds sound = new Sounds();
        public KalamburyPage()
        {
            MainPage.isMainPage = false;
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
            sound.ClickSound();
            MenuView.IsVisible = true;
            LeftMenu.IsVisible = false;
            await MenuView.TranslateTo(0, 0, 500, Easing.CubicOut);

        }

        private async void OnCloseMenuClicked(object sender, EventArgs e)
        {
            sound.ClickSound();
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
        private async void AnimalsButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Animals";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void AdultMixed_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "AdultMixed";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void CelebritiesButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Celebrities";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void DailyLifeButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "DailyLife";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void PolandButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Poland";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void RhymesButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Rhymes";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void SportsButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "Sports";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }

        private async void ScienceTopicsButton_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            MainPage.gameMode = "ScienceTopics";
            MainPage.isMainPage = false;
            await Navigation.PushAsync(new BeforeGameKalambury());
        }
        private async void Multiplayer_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            await Navigation.PushAsync(new RankingPage());
        }

        private async void Logo_Clicked(object sender, EventArgs e)
        {
            sound.Bomb();
            Logo.IsEnabled = false;

            // List of other buttons
            var otherButtons = new List<ImageButton>
    {
        dladzieciButton, emocjeButton, filmyButton, bajkiButton, zawodyButton, AnimalsButton,
        AdultMixed, PolandButton, ScienceTopicsButton, netflixButton, panstwaButton,
        przyslowiaButton, fictionButton, postaciehistoryczne, CelebritiesButton, DailyLifeButton,
        RhymesButton, SportsButton
    };

            await Logo.ScaleTo(0.6, 500, Easing.CubicIn);

            // Add a short "shake" effect by quickly scaling up and down
            await Logo.ScaleTo(0.7, 80, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 80, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 80, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 80, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 80, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 80, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 70, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 70, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 70, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 70, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 70, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 70, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 70, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 70, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 60, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 60, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 60, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 60, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 60, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 60, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 60, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 60, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 50, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 50, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 50, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 50, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 50, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 50, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 50, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 50, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 40, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 40, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 40, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 40, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 30, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 30, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 30, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 30, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 30, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 30, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 30, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 30, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 30, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 30, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 30, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 30, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 30, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 30, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 30, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 30, Easing.CubicIn);
            await Logo.ScaleTo(0.7, 30, Easing.CubicOut);
            await Logo.ScaleTo(0.65, 30, Easing.CubicIn);
            await Logo.ScaleTo(0.65, 1000, Easing.CubicOut);

            // Move other buttons outward
            foreach (var button in otherButtons)
            {
                var randomX = new Random().Next(-1000, 1000); // Random X movement
                var randomY = new Random().Next(-1000, 1000); // Random Y movement
                _ = button.TranslateTo(randomX, randomY, 500, Easing.CubicOut);
            }

            // Scale Logo up
            await Logo.ScaleTo(1.6, 500, Easing.CubicOut);

            // Wait and scale Logo down
            await Logo.ScaleTo(1.3, 100, Easing.CubicIn);

            // Bring other buttons back to original positions
            foreach (var button in otherButtons)
            {
                _ = button.TranslateTo(0, 0, 2000, Easing.CubicOut);
            }

            // Rotate Logo
            await Logo.RotateTo(360, 1500); // Rotate to 360 degrees in 1.5 seconds
            Logo.Rotation = 0; // Reset rotation angle

            Logo.IsEnabled = true;
        }

    }
}
