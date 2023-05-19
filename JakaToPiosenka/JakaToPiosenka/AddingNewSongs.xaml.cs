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
            SongsCollection.ItemsSource = MainPage.connection.Table<FairyTales>().ToList<FairyTales>();
            switch (MainPage.gameMode)
            {
                case "allSongs":
                    SongsCollection.ItemsSource = MainPage.connection.Table<AllSongs>().ToList<AllSongs>();
                    break;
                case "Disney":
                    SongsCollection.ItemsSource = MainPage.connection.Table<FairyTales>().ToList<FairyTales>();
                    break;
                case "Pop":
                    SongsCollection.ItemsSource = MainPage.connection.Table<Pop>().ToList<Pop>();
                    break;
                case "Rock":
                    SongsCollection.ItemsSource = MainPage.connection.Table<Rock>().ToList<Rock>();
                    break;
                case "UsersMusic":
                    SongsCollection.ItemsSource = MainPage.connection.Table<UsersMusic>().ToList<UsersMusic>();
                    break;
                case "Rap":
                    SongsCollection.ItemsSource = MainPage.connection.Table<Rap>().ToList<Rap>();
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
                        MainPage.connection.Insert(songsDataallSongs);
                        MainPage.connectionRestart.Insert(songsDataallSongs);
                        break;
                    case "Disney":
                        var songsDataDisney = new FairyTales
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        MainPage.connection.Insert(songsDataDisney);
                        MainPage.connectionRestart.Insert(songsDataDisney);
                        break;
                    case "Pop":
                        var songsDataPop = new Pop
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        MainPage.connection.Insert(songsDataPop);
                        MainPage.connectionRestart.Insert(songsDataPop);
                        break;
                    case "Rock":
                        var songsDataRock = new Rock
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        MainPage.connection.Insert(songsDataRock);
                        MainPage.connectionRestart.Insert(songsDataRock);
                        break;
                    case "UsersMusic":
                        var songsDataUsersMusic = new UsersMusic
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        MainPage.connection.Insert(songsDataUsersMusic);
                        MainPage.connectionRestart.Insert(songsDataUsersMusic);
                        break;
                    case "Rap":
                        var songsDataRap = new Rap
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        MainPage.connection.Insert(songsDataRap);
                        MainPage.connectionRestart.Insert(songsDataRap);
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