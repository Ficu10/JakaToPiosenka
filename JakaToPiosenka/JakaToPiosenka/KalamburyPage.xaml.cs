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
        private double maxScrollPosition = 550; // Maximum scroll position

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

        private void zwierzetaButton_Clicked(object sender, EventArgs e)
        {

        }

        private void przyslowiaButton_Clicked(object sender, EventArgs e)
        {

        }

        private void panstwaButton_Clicked(object sender, EventArgs e)
        {

        }

        private void netflixButton_Clicked(object sender, EventArgs e)
        {

        }

        private void bajkiButton_Clicked(object sender, EventArgs e)
        {

        }

        private void filmyButton_Clicked(object sender, EventArgs e)
        {

        }

        private void emocjeButton_Clicked(object sender, EventArgs e)
        {

        }

        private void dladzieciButton_Clicked(object sender, EventArgs e)
        {

        }

        private void postaciehistoryczne_Clicked(object sender, EventArgs e)
        {

        }

        private void zawodyButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
