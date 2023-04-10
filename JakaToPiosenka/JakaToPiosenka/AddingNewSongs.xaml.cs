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
            //switch (MainPage.gameMode)
            //{
            //    case "allSongs":
            //        SongsCollection.ItemsSource = Game.songsTabRestart;
            //        break;
            //    case "Disney":
            //        SongsCollection.ItemsSource = Game.songsTabFairyTalesRestart;
            //        break;
            //    case "Pop":
            //        SongsCollection.ItemsSource = Game.songsTabPopRestart;
            //        break;
            //    case "Rock":
            //        SongsCollection.ItemsSource = Game.songsTabRockRestart;
            //        break;
            //    case "UsersMusic":
            //        SongsCollection.ItemsSource = Game.songsTabUsersMusicRestart;
            //        break;
            //    case "Rap":
            //        SongsCollection.ItemsSource = Game.songsTabRapRestart;
            //        break;

            //}
        }
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                SongsCollection.ItemsSource = await App.MyDatabase.ReadSongsAndAuthors();
            }
            catch { }
        }


        private async void BackButtonn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeforeGamePage());
        }

        private async void addSongToList_Clicked(object sender, EventArgs e)
        {
            if (NewSongName.Text != "" && NewAuthorName.Text != "")
            {
                switch (MainPage.gameMode)
                {
                    case "allSongs":
                        Game.songsTabRestart.Add(NewSongName.Text);
                        Game.authorsTabRestart.Add(NewAuthorName.Text);
                        break;
                    case "Disney":
                        Game.songsTabFairyTalesRestart.Add(NewSongName.Text);
                        Game.authorsTabFairyTalesRestart.Add(NewAuthorName.Text);
                        break;
                    case "Pop":
                        Game.songsTabPopRestart.Add(NewSongName.Text);
                        Game.authorsTabPopRestart.Add(NewAuthorName.Text);
                        break;
                    case "Rock":
                        Game.songsTabRockRestart.Add(NewSongName.Text);
                        Game.authorsTabRockRestart.Add(NewAuthorName.Text);
                        break;
                    case "UsersMusic":
                        Game.songsTabUsersMusicRestart.Add(NewSongName.Text);
                        Game.authorsTabUsersMusicRestart.Add(NewAuthorName.Text);
                        break;
                    case "Rap":
                        Game.songsTabRapRestart.Add(NewSongName.Text);
                        Game.authorsTabRapRestart.Add(NewAuthorName.Text);
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

            searchTerm = searchTerm.ToLowerInvariant();

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