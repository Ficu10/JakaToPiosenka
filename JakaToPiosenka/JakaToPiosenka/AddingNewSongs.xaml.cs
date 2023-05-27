using JakaToPiosenka.MusicClasses;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using static SQLite.SQLite3;
using static System.Net.Mime.MediaTypeNames;

namespace JakaToPiosenka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddingNewSongs : ContentPage
    {
        Sounds sound = new Sounds();

        public AddingNewSongs()
        {
            InitializeComponent();
            SongsCollection.ItemsSource = MusicTypes.connection.Table<Pop>().ToList<Pop>();
            switch (MainPage.gameMode)
            {
                case "AllSongs":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<AllSongs>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "FairyTales":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<FairyTales>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "Pop":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Pop>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "Rock":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Rock>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "UsersMusic":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<UsersMusic>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "Rap":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Rap>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "RapPolish":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RapPolish>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "RapEnglish":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RapEnglish>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "PopPolish":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<PopPolish>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "PopEnglish":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<PopEnglish>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "The80":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<The80>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "The80Polish":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<The80Polish>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "The80English":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<The80English>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "RockPolish":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RockPolish>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "RockEnglish":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RockEnglish>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "Youtube":
                    SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Youtube>()
                        .OrderBy(song => song.Author)
                        .ThenBy(song => song.Title)
                        .ToList();
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
            sound.ClickSound();
            if (NewSongName.Text != "" && NewAuthorName.Text != "")
            {
                // Define the mapping between game modes and table types
                Dictionary<string, Type> tableTypeMap = new Dictionary<string, Type>
                {
                    { "AllSongs", typeof(AllSongs) },
                    { "FairyTales", typeof(FairyTales) },
                    { "Pop", typeof(Pop) },
                    { "Rock", typeof(Rock) },
                    { "UsersMusic", typeof(UsersMusic) },
                    { "Rap", typeof(Rap) },
                    { "The80", typeof(The80) },
                    { "The80English", typeof(The80English) },
                    { "The80Polish", typeof(The80Polish) },
                    { "RapEnglish", typeof(RapEnglish) },
                    { "RapPolish", typeof(RapPolish) },
                    { "PopEnglish", typeof(PopEnglish) },
                    { "PopPolish", typeof(PopPolish) },
                    { "RockEnglish", typeof(RockEnglish) },
                    { "RockPolish", typeof(RockPolish) },
                    { "Youtube", typeof(Youtube) },
                };

                if (tableTypeMap.ContainsKey(MainPage.gameMode))
                {
                    var tableType = tableTypeMap[MainPage.gameMode];
                    var songsData = Activator.CreateInstance(tableType);

                    tableType.GetProperty("Title").SetValue(songsData, NewSongName.Text);
                    tableType.GetProperty("Author").SetValue(songsData, NewAuthorName.Text);

                    MusicTypes.connection.Insert(songsData);
                    MusicTypes.connectionRestart.Insert(songsData);
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
            var item = sender as SwipeItem;
            var emp = item.CommandParameter as MusicTypes;

            Type musicType = GetMusicTypeByGameMode(MainPage.gameMode);
            if (musicType != null)
            {
                emp = item.CommandParameter as MusicTypes;
                emp = Convert.ChangeType(emp, musicType) as MusicTypes;
            }

            bool result = await DisplayAlert("Usuń", $"Czy chcesz usunąć piosenkę: {emp.Title}?", "tak", "nie");
            if (result)
            {
                sound.DeleteSound();
                string titleToRemove = emp.Title;
                string authorToRemove = emp.Author;

                string deleteQuery = $"DELETE FROM {MainPage.gameMode} WHERE Title = ? AND Author = ?";
                MusicTypes.connection.Execute(deleteQuery, titleToRemove, authorToRemove);
                MusicTypes.connectionRestart.Execute(deleteQuery, titleToRemove, authorToRemove);
                await Navigation.PushAsync(new AddingNewSongs());
            }
        }

        private Type GetMusicTypeByGameMode(string gameMode)
        {
            string typeName = gameMode;
            if (typeName.StartsWith("The80"))
            {
                typeName = typeName.Replace("The80", "The80");
            }

            typeName = char.ToUpper(typeName[0]) + typeName.Substring(1);
            typeName += gameMode.EndsWith("English") ? "English" : "Polish";
            typeName = "JakaToPiosenka." + typeName;

            Type type = Type.GetType(typeName);
            return type;
        }




        private async void Import_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status == PermissionStatus.Granted)
            {
                ImportMethod();
            }
            else if (status == PermissionStatus.Denied)
            {
                await App.Current.MainPage.DisplayAlert("Eksport nie powiódł się", "Sprawdź zezwoleniach aplikacji, czy można używać pamięci wewnętrznej", "OK");
            }
            else if (status == PermissionStatus.Unknown)
            {
                await App.Current.MainPage.DisplayAlert("Eksport nie powiódł się", "Sprawdź zezwoleniach aplikacji, czy można używać pamięci wewnętrznej", "OK");
            }
            ImportMethod();
        }

        private void Eksport_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();

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
            else if (MainPage.gameMode == "RapPolish")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<RapPolish>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<RapPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RapEnglish")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<RapEnglish>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<RapEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "PopPolish")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<PopPolish>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<PopPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "PopEnglish")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<PopEnglish>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<PopEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<The80>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<The80>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80English")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<The80English>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<The80English>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80Polish")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<The80Polish>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<The80Polish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RockEnglish")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<RockEnglish>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<RockEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RockPolish")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<RockPolish>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<RockPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Youtube")
            {
                ExportMethod(MusicTypes.connectionRestart.Table<Youtube>().ToList().Select(x => x.Author).ToList(), MusicTypes.connectionRestart.Table<Youtube>().ToList().Select(x => x.Title).ToList());
            }
        }


        private async void ExportMethod(List<string> authorsList, List<string> songsList)
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status == PermissionStatus.Granted)
            {
                string result = await DisplayPromptAsync("Eksport", "Wpisz nazwę pliku:", "OK", "Anuluj", placeholder: "Wprowadź tekst");

                if (!string.IsNullOrEmpty(result))
                {
                    string fileName = result + "JakaToPiosenka.txt";


                    //string documentsPath = FileSystem.AppDataDirectory;
                    string documentsPath = "/storage/emulated/0/Documents";




                    string filePath = System.IO.Path.Combine(documentsPath, fileName);

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        for (int i = 0; i < authorsList.Count(); i++)
                        {
                            await writer.WriteLineAsync(authorsList[i] + ";" + songsList[i]);
                        }
                    }
                    await App.Current.MainPage.DisplayAlert("Eksport powiódł się", "Plik został wyeksportowany pomyślnie", "OK");

                    await Share.RequestAsync(new ShareFileRequest
                    {
                        Title = "Eksportuj",
                        File = new ShareFile(filePath)
                    });
                }
            }
            else if (status == PermissionStatus.Denied)
            {
                await App.Current.MainPage.DisplayAlert("Eksport nie powiódł się", "Sprawdź zezwoleniach aplikacji, czy można używać pamięci wewnętrznej", "OK");
            }
            else if (status == PermissionStatus.Unknown)
            {
                await App.Current.MainPage.DisplayAlert("Eksport nie powiódł się", "Sprawdź zezwoleniach aplikacji, czy można używać pamięci wewnętrznej", "OK");
            }
           
        }
        private async void ImportMethod()
        {

            var file = await FilePicker.PickAsync();

            if (file != null)
            {
                if (MainPage.gameMode == "AllSongs")
                {
                    MusicTypes.connection.DeleteAll<AllSongs>();
                    MusicTypes.connectionRestart.DeleteAll<AllSongs>();
                }
                else if (MainPage.gameMode == "FairyTales")
                {
                    MusicTypes.connection.DeleteAll<FairyTales>();
                    MusicTypes.connectionRestart.DeleteAll<FairyTales>();
                }
                else if (MainPage.gameMode == "Pop")
                {
                    MusicTypes.connection.DeleteAll<Pop>();
                    MusicTypes.connectionRestart.DeleteAll<Pop>();
                }
                else if (MainPage.gameMode == "Rock")
                {
                    MusicTypes.connection.DeleteAll<Rock>();
                    MusicTypes.connectionRestart.DeleteAll<Rock>();
                }
                else if (MainPage.gameMode == "UsersMusic")
                {
                    MusicTypes.connection.DeleteAll<UsersMusic>();
                    MusicTypes.connectionRestart.DeleteAll<UsersMusic>();
                }
                else if (MainPage.gameMode == "Rap")
                {
                    MusicTypes.connection.DeleteAll<Rap>();
                    MusicTypes.connectionRestart.DeleteAll<Rap>();
                }
                else if (MainPage.gameMode == "RapPolish")
                {
                    MusicTypes.connection.DeleteAll<RapPolish>();
                    MusicTypes.connectionRestart.DeleteAll<RapPolish>();
                }
                else if (MainPage.gameMode == "RapEnglish")
                {
                    MusicTypes.connection.DeleteAll<RapEnglish>();
                    MusicTypes.connectionRestart.DeleteAll<RapEnglish>();
                }
                else if (MainPage.gameMode == "PopPolish")
                {
                    MusicTypes.connection.DeleteAll<PopPolish>();
                    MusicTypes.connectionRestart.DeleteAll<PopPolish>();
                }
                else if (MainPage.gameMode == "PopEnglish")
                {
                    MusicTypes.connection.DeleteAll<PopEnglish>();
                    MusicTypes.connectionRestart.DeleteAll<PopEnglish>();
                }
                else if (MainPage.gameMode == "The80")
                {
                    MusicTypes.connection.DeleteAll<The80>();
                    MusicTypes.connectionRestart.DeleteAll<The80>();
                }
                else if (MainPage.gameMode == "The80English")
                {
                    MusicTypes.connection.DeleteAll<The80English>();
                    MusicTypes.connectionRestart.DeleteAll<The80English>();
                }
                else if (MainPage.gameMode == "The80Polish")
                {
                    MusicTypes.connection.DeleteAll<The80Polish>();
                    MusicTypes.connectionRestart.DeleteAll<The80Polish>();
                }
                else if (MainPage.gameMode == "RockEnglish")
                {
                    MusicTypes.connection.DeleteAll<RockEnglish>();
                    MusicTypes.connectionRestart.DeleteAll<RockEnglish>();
                }
                else if (MainPage.gameMode == "RockPolish")
                {
                    MusicTypes.connection.DeleteAll<RockPolish>();
                    MusicTypes.connectionRestart.DeleteAll<RockPolish>();
                }
                else if (MainPage.gameMode == "Youtube")
                {
                    MusicTypes.connection.DeleteAll<Youtube>();
                    MusicTypes.connectionRestart.DeleteAll<Youtube>();
                }
               
                using (Stream stream = await file.OpenReadAsync())
                {
                    using (StreamReader reader2 = new StreamReader(stream))
                    {

                        string line;


                        while ((line = reader2.ReadLine()) != null)
                        {
                            string[] fields = line.Split(';');
                            if (fields.Length == 2)
                            {
                                if (MainPage.gameMode == "AllSongs")
                                {
                                    var songsData = new AllSongs
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "FairyTales")
                                {
                                    var songsData = new FairyTales
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "Pop")
                                {
                                    var songsData = new Pop
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "Rock")
                                {
                                    var songsData = new Rock
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "UsersMusic")
                                {
                                    var songsData = new UsersMusic
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "Rap")
                                {
                                    var songsData = new Rap
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "RapPolish")
                                {
                                    var songsData = new RapPolish
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "RapEnglish")
                                {
                                    var songsData = new RapEnglish
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "PopPolish")
                                {
                                    var songsData = new PopPolish
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "PopEnglish")
                                {
                                    var songsData = new PopEnglish
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "The80")
                                {
                                    var songsData = new The80
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "The80English")
                                {
                                    var songsData = new The80English
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "The80Polish")
                                {
                                    var songsData = new The80Polish
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "RockEnglish")
                                {
                                    var songsData = new RockEnglish
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "RockPolish")
                                {
                                    var songsData = new RockPolish
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }
                                else if (MainPage.gameMode == "Youtube")
                                {
                                    var songsData = new Youtube
                                    {
                                        Title = fields[1],
                                        Author = fields[0]
                                    };
                                    MusicTypes.connection.Insert(songsData);
                                    MusicTypes.connectionRestart.Insert(songsData);
                                }



                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Błąd pliku", "Nieprawidłowy format pliku", "OK");
                                }
                            }
                        }


                        await Navigation.PushAsync(new AddingNewSongs());


                    }
                }
            }











        }

     
        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;

            if (string.IsNullOrEmpty(searchText))
            {
                // Show all items when search text is empty
                switch (MainPage.gameMode)
                {
                    case "AllSongs":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<AllSongs>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "FairyTales":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<FairyTales>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "Pop":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Pop>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "Rock":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Rock>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "UsersMusic":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<UsersMusic>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "Rap":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Rap>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "RapPolish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RapPolish>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "RapEnglish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RapEnglish>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "PopPolish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<PopPolish>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "PopEnglish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<PopEnglish>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "The80":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<The80>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "The80Polish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<The80Polish>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "The80English":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<The80English>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "RockPolish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RockPolish>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "RockEnglish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RockEnglish>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "Youtube":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Youtube>()
                            .OrderBy(song => song.Author)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                }
            }
            else
            {
                switch (MainPage.gameMode)
                {
                    case "AllSongs":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<AllSongs>()
                           .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "FairyTales":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<FairyTales>()
                          .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "Pop":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Pop>()
                           .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "Rock":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Rock>()
                           .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "UsersMusic":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<UsersMusic>()
                           .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "Rap":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Rap>()
                           .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "RapPolish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RapPolish>()
                           .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "RapEnglish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RapEnglish>()
                           .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "PopPolish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<PopPolish>()
                           .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "PopEnglish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<PopEnglish>()
                           .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "The80":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<The80>()
                           .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "The80Polish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<The80Polish>()
                            .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "The80English":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<The80English>()
                            .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "RockPolish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RockPolish>()
                          .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "RockEnglish":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<RockEnglish>()
                            .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "Youtube":
                        SongsCollection.ItemsSource = MusicTypes.connectionRestart.Table<Youtube>()
                            .Where(song => song.Author.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                }



            }
        }
    }
}