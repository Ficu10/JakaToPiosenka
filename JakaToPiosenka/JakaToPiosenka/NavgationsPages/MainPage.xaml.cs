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
using JakaToPiosenka.KalamburyClasses;
using JakaToPiosenka.MusicClasses;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace JakaToPiosenka
{
    public partial class MainPage : ContentPage
    {
        public static bool orientationPortrait = true;
        public static string gameMode = "allSongs";
        public static bool isMainPage = true;

        private bool isScrollLocked = false; // Flag to indicate if scrolling is locked
        private double maxScrollPosition = 1350; // Maximum scroll position

        Sounds sound = new Sounds();
        public MainPage()
        {
            isMainPage = true;
           
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
            var AllPasswordsSongs = new List<AllPasswords>()
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
                new Youtube(),
                new Children(),
                new Countries(),
                new Emotions(),
                new FictionalCharacter(),
                new HistoricalCharacter(),
                new Jobs(),
                new Movies(),
                new Series(),
                new Tales(),
                new AdultMixed(),
                new Animals(),
                new Celebrities(),
                new DailyLife(),
                new Poland(),
                new Rhymes(),
                new ScienceTopics(),
                new Sports(),
                new Carols(),
                new ChristmasSongs(),
                new Words()

            };

            bool isTableCreated = AllPasswords.connection.GetTableInfo("Pop").Any();
            if (!isTableCreated)
            {
                foreach (var item in AllPasswordsSongs)
                {
                    item.Load();
                }
            }
            isMainPage = true;

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
        private async void Koledy_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "Carols";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private async void Christmas_Songs_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            gameMode = "ChristmasSongs";
            await Navigation.PushAsync(new BeforeGamePage());
        }
        private async void Multiplayer_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            await Navigation.PushAsync(new RankingPage());
        }
        private async void Logo_Clicked(object sender, EventArgs e)
        {
            Logo.IsEnabled= false;
            sound.Drum(); // Play the drum sound

            // List of buttons and image buttons to animate
            var buttons = new List<Xamarin.Forms.VisualElement> // Explicitly use Xamarin.Forms.VisualElement
    {
        AllSongsButton,
        RockButton,
        PopButton,
        DisneyButton,
        RapButton,
        UsersMusicButton,
        The80Button,
        Koledy,
        The80PolishButton,
        RockPolishButton,
        RockEnglishButton,
        RapPolishButton,
        RapEnglishButton,
        PopPolishButton,
        PopEnglishButton,
        The80English,
        Christmas_Songs,
        Logo,
    };

            double beatDuration = 470; // 470ms for a beat

            // Scale and sync to the rhythm
            for (int i = 0; i < 7; i++)
            {
                await Task.WhenAll(buttons.Select(button =>
                    button.ScaleTo(0.8, (uint)(beatDuration / 2), Easing.CubicOut)));
                await Task.WhenAll(buttons.Select(button =>
                    button.ScaleTo(1.1, (uint)(beatDuration / 2), Easing.CubicIn)));
            }

            for (int i = 0; i < 4; i++)
            {
                await Task.WhenAll(buttons.Select(button =>
                    button.ScaleTo(0.8, (uint)(beatDuration / 8), Easing.CubicOut)));
                await Task.WhenAll(buttons.Select(button =>
                    button.ScaleTo(1.1, (uint)(beatDuration / 8), Easing.CubicIn)));
            }

            for (int i = 0; i < 6; i++)
            {
                await Task.WhenAll(buttons.Select(button =>
                    button.ScaleTo(0.8, (uint)(beatDuration / 2), Easing.CubicOut)));
                await Task.WhenAll(buttons.Select(button =>
                    button.ScaleTo(1.1, (uint)(beatDuration / 2), Easing.CubicIn)));
            }

            // Final scaling effect
            await Task.WhenAll(buttons.Select(button => button.ScaleTo(1.3, 200, Easing.CubicOut)));
            await Task.WhenAll(buttons.Select(button => button.ScaleTo(1.05, 200, Easing.CubicIn)));

            // Rotate all buttons
            await Task.WhenAll(buttons.Select(button => button.RotateTo(360, 500))); // Rotate all to 360 degrees
            await Logo.ScaleTo(1.3, 100, Easing.CubicInOut);
            foreach (var button in buttons)
            {
                button.Rotation = 0; // Reset rotation angle
            }
            Logo.IsEnabled = true;
        }




    }
}
