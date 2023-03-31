using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JakaToPiosenka
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddingNewSongs : ContentPage
	{
		public AddingNewSongs ()
		{
			InitializeComponent ();
            switch (MainPage.gameMode)
            {
                case "allSongs":
                    SongsCollection.ItemsSource = Game.songsTab;
                    break;
                case "Disney":
                    SongsCollection.ItemsSource = Game.songsTabFairyTales;
                    break;
                case "Pop":
                    SongsCollection.ItemsSource = Game.songsTabPop;
                    break;
                case "Rock":
                    SongsCollection.ItemsSource = Game.songsTabRock;
                    break;
                case "UsersMusic":
                    SongsCollection.ItemsSource = Game.songsTabUsersMusic;
                    break;
                case "Rap":
                    SongsCollection.ItemsSource = Game.songsTabRap;
                    break;

            }
        }

        private async void addSong_Clicked(object sender, EventArgs e)
        {

                switch (MainPage.gameMode)
                {
                    case "allSongs":
                        Game.songsTab.Add(NewSongName.Text);
                        Game.authorsTab.Add(NewAuthorName.Text);
                        break;
                    case "Disney":
                        Game.songsTabFairyTales.Add(NewSongName.Text);
                        Game.authorsTabFairyTales.Add(NewAuthorName.Text);
                        break;
                    case "Pop":
                        Game.songsTabPop.Add(NewSongName.Text);
                        Game.authorsTabPop.Add(NewAuthorName.Text);
                        break;
                    case "Rock":
                        Game.songsTabRock.Add(NewSongName.Text);
                        Game.authorsTabRock.Add(NewAuthorName.Text);
                        break;
                    case "UsersMusic":
                        Game.songsTabUsersMusic.Add(NewSongName.Text);
                        Game.authorsTabUsersMusic.Add(NewAuthorName.Text);
                        break;
                    case "Rap":
                        Game.songsTabRap.Add(NewSongName.Text);
                        Game.authorsTabRap.Add(NewAuthorName.Text);
                        break;

                }
            await Navigation.PushAsync(new BeforeGamePage());
          

        }

        private async void BackButtonn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeforeGamePage());
        }
    }
}