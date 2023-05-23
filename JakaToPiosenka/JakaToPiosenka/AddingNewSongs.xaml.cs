using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SQLite.SQLite3;
using static System.Net.Mime.MediaTypeNames;

namespace JakaToPiosenka
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddingNewSongs : ContentPage
	{
        public AddingNewSongs ()
		{
			InitializeComponent ();
            SongsCollection.ItemsSource = MusicTypes.connection.Table<Pop>().ToList<Pop>();
            switch (MainPage.gameMode)
            {
                case "AllSongs":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<AllSongs>().ToList<AllSongs>();
                    break;
                case "FairyTales":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<FairyTales>().ToList<FairyTales>();
                    break;
                case "Pop":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Pop>().ToList<Pop>();
                    break;
                case "Rock":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Rock>().ToList<Rock>();
                    break;
                case "UsersMusic":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<UsersMusic>().ToList<UsersMusic>();
                    break;  
                case "Rap":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Rap>().ToList<Rap>();
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
            //if (NewSongName.Text != "" && NewAuthorName.Text != "")
            //{
                switch (MainPage.gameMode)
                {
                    case "AllSongs":
                        var songsDataallSongs = new AllSongs
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        MusicTypes.connection.Insert(songsDataallSongs);
                        MusicTypes.connectionRestart.Insert(songsDataallSongs);
                        break;
                    case "FairyTales":
                        var songsDataDisney = new FairyTales
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        MusicTypes.connection.Insert(songsDataDisney);
                        MusicTypes.connectionRestart.Insert(songsDataDisney);
                        break;
                    case "Pop":
                        var songsDataPop = new Pop
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        MusicTypes.connection.Insert(songsDataPop);
                        MusicTypes.connectionRestart.Insert(songsDataPop);
                        break;
                    case "Rock":
                        var songsDataRock = new Rock
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        MusicTypes.connection.Insert(songsDataRock);
                        MusicTypes.connectionRestart.Insert(songsDataRock);
                        break;
                    case "UsersMusic":
                        var songsDataUsersMusic = new UsersMusic
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        MusicTypes.connection.Insert(songsDataUsersMusic);
                        MusicTypes.connectionRestart.Insert(songsDataUsersMusic);
                        break;
                    case "Rap":
                        var songsDataRap = new Rap
                        {
                            Title = NewSongName.Text,
                            Author = NewAuthorName.Text
                        };
                        MusicTypes.connection.Insert(songsDataRap);
                        MusicTypes.connectionRestart.Insert(songsDataRap);
                        break;

                }
                await Navigation.PushAsync(new AddingNewSongs());
                NewSongName.Text = "";
                NewAuthorName.Text = "";
            //}
            //else
            //{
            //    await DisplayAlert("Blad", "Prosze podac autora oraz tytul piosenki", "OK");
            //}

        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var emp = item.CommandParameter as MusicTypes;
            switch (MainPage.gameMode)
            {
                case "AllSongs":
                    emp = item.CommandParameter as AllSongs;
                    break;
                case "FairyTales":
                    emp = item.CommandParameter as FairyTales;
                    break;
                case "Pop":
                    emp = item.CommandParameter as Pop;
                    break;
                case "Rock":
                    emp = item.CommandParameter as Rock;
                    break;
                case "UsersMusic":
                    emp = item.CommandParameter as UsersMusic;
                    break;
                case "Rap":
                    emp = item.CommandParameter as Rap;
                    break;

            }
            bool result = await DisplayAlert("Usuń", $"Czy chcesz usunąć piosenkę: {emp.Title}?", "tak", "nie");
            if (result)
            {
                string titleToRemove = emp.Title;
                string authorToRemove = emp.Author;

                string deleteQuery = $"DELETE FROM {MainPage.gameMode} WHERE Title = ? AND Author = ?";
                MusicTypes.connection.Execute(deleteQuery, titleToRemove, authorToRemove);
                MusicTypes.connectionRestart.Execute(deleteQuery, titleToRemove, authorToRemove);
                await Navigation.PushAsync(new AddingNewSongs());



            }
        }

     

        private async void Import_Clicked(object sender, EventArgs e)
        {
            PickFileAsync();
            await Navigation.PushAsync(new AddingNewSongs());
        }

        private void Eksport_Clicked(object sender, EventArgs e)
        {
          
            if (MainPage.gameMode == "AllSongs")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<AllSongs>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<AllSongs>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "FairyTales")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<FairyTales>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<FairyTales>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Pop")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<Pop>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<Pop>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Rock")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<Rock>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<Rock>().ToList().Select(x => x.Title).ToList());

            }
            else if (MainPage.gameMode == "UsersMusic")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<UsersMusic>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<UsersMusic>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Rap")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<Rap>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<Rap>().ToList().Select(x => x.Title).ToList());
            }
        }
        private async void ExportMethod(List<string> authorsList, List<string> songsList)
        {
            string result = await DisplayPromptAsync("Eksport", "Wpisz nazwę pliku:", "OK", "Anuluj", placeholder: "Wprowadź tekst");

            if (!string.IsNullOrEmpty(result))
            {
                string fileName = result + "JakaToPiosenka.txt";
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    for (int i = 0; i < authorsList.Count(); i++)
                    {
                        writer.WriteLine(authorsList[i] + ";" + songsList[i]);
                    }
                }
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Eksportuj",
                    File = new ShareFile(filePath)
                });
            }
           
        }

       
        private async void PickFileAsync()
        {
            string result = await DisplayPromptAsync("Import", "Wpisz nazwę pliku:", "OK", "Anuluj", placeholder: "Wprowadź tekst");

            var musicTypesSongs = new List<MusicTypes>()
            {
                new Pop(),
                new Rock(),
                new FairyTales(),
                new AllSongs(),
                new Rap(),
                new UsersMusic()
            };
            if (MainPage.gameMode == "AllSongs")
            {
                musicTypesSongs[3].Import(result);
            }
            else if (MainPage.gameMode == "FairyTales")
            {
                musicTypesSongs[2].Import(result);
            }
            else if (MainPage.gameMode == "Pop")
            {
                musicTypesSongs[0].Import(result);

            }
            else if (MainPage.gameMode == "Rock")
            {
                musicTypesSongs[1].Import(result);

            }
            else if (MainPage.gameMode == "UsersMusic")
            {
                musicTypesSongs[5].Import(result);
            }
            else if (MainPage.gameMode == "Rap")
            {
                musicTypesSongs[4].Import(result);
            }

        }
       
    }
}