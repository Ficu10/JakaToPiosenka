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

namespace JakaToPiosenka
{
    public partial class MainPage : ContentPage
    {
        public static bool orientationPortrait = true;
        public static string gameMode = "allSongs";
        public MainPage()
        {
            MessagingCenter.Send(new OrientationMessage { IsLandscape = false }, "SetOrientation");

            InitializeComponent();
            var musicTypesSongs = new List<MusicTypes>()
            {
                new Pop(),
                new Rock(),
                new FairyTales(),
                new AllSongs(),
                new Rap(),
                new UsersMusic()
            };
            foreach (var item in musicTypesSongs)
            {
                item.Load();
            }

        }

      
        async void AllSongsButton_Clicked(object sender, EventArgs e)
        {
            gameMode = "allSongs";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        async void RockButton_Clicked(object sender, EventArgs e)
        {
            gameMode = "Rock";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        async void PopButton_Clicked(object sender, EventArgs e)
        {
            gameMode = "Pop";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        async void DisneyButton_Clicked(object sender, EventArgs e)
        {
            gameMode = "Disney";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        async void UsersMusicButton_Clicked(object sender, EventArgs e)
        {
            gameMode = "UsersMusic";
            await Navigation.PushAsync(new BeforeGamePage());
        }

        async void RapButton_Clicked(object sender, EventArgs e)
        {
            gameMode = "Rap";
            await Navigation.PushAsync(new BeforeGamePage());
        }
        protected override bool OnBackButtonPressed()
        {
          
            return true;
        }
    }
}
