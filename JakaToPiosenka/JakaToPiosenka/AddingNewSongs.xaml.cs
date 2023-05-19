using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            SongsCollection.ItemsSource = AllSongs.allSongsList;
            switch (MainPage.gameMode)
            {
                case "allSongs":
                    SongsCollection.ItemsSource = AllSongs.allSongsList;
                    break;
                case "Disney":
                    SongsCollection.ItemsSource = FairyTales.fairyTalesSongsList;
                    break;
                case "Pop":
                    SongsCollection.ItemsSource = Pop.popSongsList;
                    break;
                case "Rock":
                    SongsCollection.ItemsSource = Rock.rockSongsList;
                    break;
                case "UsersMusic":
                    SongsCollection.ItemsSource = AllSongs.allSongsList;
                    break;
                case "Rap":
                    SongsCollection.ItemsSource = Rap.rapSongsList;
                    break;

            }
        }
      

       
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new BeforeGamePage());
            });

            return true;
        }

        private async void addSongToList_Clicked(object sender, EventArgs e)
        {
            if (NewSongName.Text != "" && NewAuthorName.Text != "")
            {
                switch (MainPage.gameMode)
                {
                    case "allSongs":
                        var songsDataallSongs = new AllSongs
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        AllSongs.dbAllSongsRestart.Insert(songsDataallSongs);
                        break;
                    case "Disney":
                        var songsDataDisney = new FairyTales
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        FairyTales.dbFairyTalesRestart.Insert(songsDataDisney);
                        break;
                    case "Pop":
                        var songsDataPop = new Pop
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        Pop.dbPopRestart.Insert(songsDataPop);
                        break;
                    case "Rock":
                        var songsDataRock = new Rock
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        Rock.dbRockRestart.Insert(songsDataRock);
                        break;
                    case "UsersMusic":
                        var songsDataUsersMusic = new UsersMusic
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        UsersMusic.dbUsersMusicRestart.Insert(songsDataUsersMusic);
                        break;
                    case "Rap":
                        var songsDataRap = new Rap
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        Rap.dbRapRestart.Insert(songsDataRap);
                        break;

                }
                await Navigation.PushAsync(new AddingNewSongs());
                NewSongName.Text = "";
                NewAuthorName.Text = ""; 
            }
            else
            {
                await DisplayAlert("Blad", "Prosze podac autora oraz tytul piosenki", "OK");        
            }
           
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            //var item = sender as SwipeItem;
            //var emp = item.CommandParameter as SongsAndAuthors;
            //bool result = await DisplayAlert("Usuń", $"Czy chcesz usunąć bieg, który odbył się w {emp.Date}?", "tak", "nie");
            //if (result)
            //{


            //    await App.MyDatabase.DeleteHistory(emp);
            //    listViewHistory.ItemsSource = await App.MyDatabase.ReadHistory();


            //}
            await Navigation.PushAsync(new AddingNewSongs());
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = string.Empty;
            }

            searchTerm = searchTerm.ToLowerInvariant(); ///

            var filteredItems = Game.authorsTab.Where(value => value.ToLowerInvariant().Contains(searchTerm)).ToList();

            foreach (var value in Game.authorsTab)
            {
                if (!filteredItems.Contains(value))
                {
                    Game.authorsTab.Remove(value);
                }
                else if (!Game.authorsTab.Contains(value))
                {
                    Game.authorsTab.Add(value);
                }
            }
        }
    }
}