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
            LoadFieTxt();

        }

        private void LoadFieTxt()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            GetSourceFromTxt(Game.authorsTabRestart, Game.songsTabRestart, assembly.GetManifestResourceStream("JakaToPiosenka.allSongs.txt"));
            GetSourceFromTxt(Game.authorsTabRockRestart, Game.songsTabRockRestart, assembly.GetManifestResourceStream("JakaToPiosenka.Rock.txt"));
            GetSourceFromTxt(Game.authorsTabPopRestart, Game.songsTabPopRestart, assembly.GetManifestResourceStream("JakaToPiosenka.Pop.txt"));
            GetSourceFromTxt(Game.authorsTabFairyTalesRestart, Game.songsTabFairyTalesRestart, assembly.GetManifestResourceStream("JakaToPiosenka.FairyTales.txt"));
            GetSourceFromTxt(Game.authorsTabRapRestart, Game.songsTabRapRestart, assembly.GetManifestResourceStream("JakaToPiosenka.Rap.txt"));
        }
        private void GetSourceFromTxt(List<string> authors, List<string> songs, System.IO.Stream filePath)
        {
            if (authors.Count == 0 && songs.Count == 0)
            {
                var assembly = typeof(MainPage).GetTypeInfo().Assembly;
                //var filePath = assembly.GetManifestResourceStream("JakaToPiosenka.allSongs.txt");
                using (var streamReader = new StreamReader(filePath))
                {
                    int i = 0;
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var fields = line.Split(';');
                        var employee = new SongsAndAuthors
                        {
                            Title = fields[1],
                            Author = fields[0]
                        };
                        authors.Add(employee.Author);
                        songs.Add(employee.Title);
                    }


                }
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


        private void Settings_Clicked(object sender, EventArgs e)
        {

        }
    }
}
