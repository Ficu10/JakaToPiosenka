using JakaToPiosenka.HelpClasses;
using JakaToPiosenka.KalamburyClasses;
using JakaToPiosenka.MusicClasses;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
           

            SongsCollection.ItemsSource = AllPasswords.connection.Table<Pop>().ToList<Pop>();
            if (MainPage.isMainPage)
            {
                BackgroundColor = Color.FromHex("#77b3d1");
                SearchEntry.BackgroundColor = Color.FromHex("#a8d6ed");
                addSongToList.BackgroundColor = Color.FromHex("#0d5b82");
                NewPromptName.BackgroundColor = Color.FromHex("#a8d6ed");
                NewTitleName.BackgroundColor = Color.FromHex("#a8d6ed");

            }
            else
            {
                BackgroundColor = Color.FromHex("#dba348");
                SearchEntry.BackgroundColor = Color.FromHex("#e6c38c");
                addSongToList.BackgroundColor = Color.FromHex("#fc9d03");
                NewPromptName.BackgroundColor = Color.FromHex("#e6c38c");
                NewTitleName.BackgroundColor = Color.FromHex("#e6c38c");

            }
            switch (MainPage.gameMode)
            {
                case "AllSongs":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<AllSongs>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "FairyTales":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<FairyTales>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "Pop":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Pop>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "Rock":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Rock>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "UsersMusic":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<UsersMusic>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "Rap":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Rap>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "RapPolish":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RapPolish>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "RapEnglish":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RapEnglish>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "PopPolish":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<PopPolish>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "PopEnglish":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<PopEnglish>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "The80":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<The80>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "The80Polish":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<The80Polish>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "The80English":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<The80English>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "RockPolish":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RockPolish>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "RockEnglish":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RockEnglish>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "Youtube":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Youtube>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "Children":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Children>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;
                case "Countries":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Countries>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Emotions":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Emotions>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "FictionalCharacter":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<FictionalCharacter>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "HistoricalCharacter":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<HistoricalCharacter>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Jobs":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Jobs>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Movies":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Movies>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Series":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Series>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Tales":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Tales>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Words":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Words>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Carols":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Carols>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "ChristmasSongs":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<ChristmasSongs>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Animals":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Animals>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "AdultMixed":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<AdultMixed>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Celebrities":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Celebrities>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "DailyLife":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<DailyLife>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Poland":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Poland>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Rhymes":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Rhymes>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "ScienceTopics":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<ScienceTopics>()
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                    break;

                case "Sports":
                    SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Sports>()
                        .OrderBy(song => song.Prompt)
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
            if (NewTitleName.Text != "" && NewPromptName.Text != "")
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
                    { "Children", typeof(Children) },
                    { "Countries", typeof(Countries) },
                    { "Emotions", typeof(Emotions) },
                    { "FictionalCharacter", typeof(FictionalCharacter) },
                    { "HistoricalCharacter", typeof(HistoricalCharacter) },
                    { "Jobs", typeof(Jobs) },
                    { "Movies", typeof(Movies) },
                    { "Series", typeof(Series) },
                    { "Tales", typeof(Tales) },
                    { "Words", typeof(Words) },
                    { "Carols", typeof(Carols) },
                    { "ChristmasSongs", typeof(ChristmasSongs) },
                    { "Animals", typeof(Animals) },
                    { "AdultMixed", typeof(AdultMixed) },
                    { "Celebrities", typeof(Celebrities) },
                    { "DailyLife", typeof(DailyLife) },
                    { "Poland", typeof(Poland) },
                    { "Rhymes", typeof(Rhymes) },
                    { "ScienceTopics", typeof(ScienceTopics) },
                    { "Sports", typeof(Sports) }
                };

                if (tableTypeMap.ContainsKey(MainPage.gameMode))
                {
                    var tableType = tableTypeMap[MainPage.gameMode];
                    var songsData = Activator.CreateInstance(tableType);

                    tableType.GetProperty("Title").SetValue(songsData, NewTitleName.Text);
                    tableType.GetProperty("Prompt").SetValue(songsData, NewPromptName.Text);

                    AllPasswords.connection.Insert(songsData);
                    AllPasswords.connectionRestart.Insert(songsData);




                }

                await Navigation.PushAsync(new AddingNewSongs());
                NewTitleName.Text = "";
                NewPromptName.Text = "";
            }
            else
            {
                if (MainPage.isMainPage)
                {
                    await DisplayAlert("Blad", "Prosze podac autora oraz tytul piosenki", "OK");

                }
                else
                {
                    await DisplayAlert("Blad", "Prosze podac hasło oraz kategorie", "OK");
                }
            }

        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {

            var item = sender as SwipeItem;
            var emp = item.CommandParameter as AllPasswords;

            Type musicType = GetMusicTypeByGameMode(MainPage.gameMode);
            if (musicType != null)
            {
                emp = item.CommandParameter as AllPasswords;
                emp = Convert.ChangeType(emp, musicType) as AllPasswords;
            }

            bool result = await DisplayAlert("Usuń", $"Czy chcesz usunąć: {emp.Title}?", "tak", "nie");
            if (result)
            {
                sound.DeleteSound();
                string titleToRemove = emp.Title;
                string promptToRemove = emp.Prompt;

                string deleteQuery = $"DELETE FROM {MainPage.gameMode} WHERE Title = ? AND Prompt = ?";
                AllPasswords.connection.Execute(deleteQuery, titleToRemove, promptToRemove);
                AllPasswords.connectionRestart.Execute(deleteQuery, titleToRemove, promptToRemove);
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
                ExportMethod(AllPasswords.connectionRestart.Table<AllSongs>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<AllSongs>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "FairyTales")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<FairyTales>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<FairyTales>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Pop")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<Pop>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Pop>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Rock")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<Rock>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Rock>().ToList().Select(x => x.Title).ToList());

            }
            else if (MainPage.gameMode == "UsersMusic")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<UsersMusic>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<UsersMusic>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Rap")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<Rap>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Rap>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RapPolish")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<RapPolish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<RapPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RapEnglish")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<RapEnglish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<RapEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "PopPolish")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<PopPolish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<PopPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "PopEnglish")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<PopEnglish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<PopEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<The80>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<The80>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80English")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<The80English>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<The80English>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80Polish")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<The80Polish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<The80Polish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RockEnglish")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<RockEnglish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<RockEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RockPolish")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<RockPolish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<RockPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Youtube")
            {
                ExportMethod(AllPasswords.connectionRestart.Table<Youtube>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Youtube>().ToList().Select(x => x.Title).ToList());
            }
        }


        private async void ExportMethod(List<string> PromptsList, List<string> songsList)
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
                        for (int i = 0; i < PromptsList.Count(); i++)
                        {
                            await writer.WriteLineAsync(PromptsList[i] + ";" + songsList[i]);
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
                await App.Current.MainPage.DisplayAlert("Eksport nie powiódł się", "Sprawdź w zezwoleniach aplikacji, czy można używać pamięci wewnętrznej", "OK");
            }
            else if (status == PermissionStatus.Unknown)
            {
                await App.Current.MainPage.DisplayAlert("Eksport nie powiódł się", "Sprawdź w zezwoleniach aplikacji, czy można używać pamięci wewnętrznej", "OK");
            }

        }
        private async void ImportMethod()
        {
            var file = await FilePicker.PickAsync();
            string fileName = file.FileName;


            if (file != null)
            {
                if (fileName.EndsWith(".txt"))
                {
                    if (MainPage.gameMode == "AllSongs")
                    {
                        AllPasswords.connection.DeleteAll<AllSongs>();
                        AllPasswords.connectionRestart.DeleteAll<AllSongs>();
                    }
                    else if (MainPage.gameMode == "FairyTales")
                    {
                        AllPasswords.connection.DeleteAll<FairyTales>();
                        AllPasswords.connectionRestart.DeleteAll<FairyTales>();
                    }
                    else if (MainPage.gameMode == "Pop")
                    {
                        AllPasswords.connection.DeleteAll<Pop>();
                        AllPasswords.connectionRestart.DeleteAll<Pop>();
                    }
                    else if (MainPage.gameMode == "Rock")
                    {
                        AllPasswords.connection.DeleteAll<Rock>();
                        AllPasswords.connectionRestart.DeleteAll<Rock>();
                    }
                    else if (MainPage.gameMode == "UsersMusic")
                    {
                        AllPasswords.connection.DeleteAll<UsersMusic>();
                        AllPasswords.connectionRestart.DeleteAll<UsersMusic>();
                    }
                    else if (MainPage.gameMode == "Rap")
                    {
                        AllPasswords.connection.DeleteAll<Rap>();
                        AllPasswords.connectionRestart.DeleteAll<Rap>();
                    }
                    else if (MainPage.gameMode == "RapPolish")
                    {
                        AllPasswords.connection.DeleteAll<RapPolish>();
                        AllPasswords.connectionRestart.DeleteAll<RapPolish>();
                    }
                    else if (MainPage.gameMode == "RapEnglish")
                    {
                        AllPasswords.connection.DeleteAll<RapEnglish>();
                        AllPasswords.connectionRestart.DeleteAll<RapEnglish>();
                    }
                    else if (MainPage.gameMode == "PopPolish")
                    {
                        AllPasswords.connection.DeleteAll<PopPolish>();
                        AllPasswords.connectionRestart.DeleteAll<PopPolish>();
                    }
                    else if (MainPage.gameMode == "PopEnglish")
                    {
                        AllPasswords.connection.DeleteAll<PopEnglish>();
                        AllPasswords.connectionRestart.DeleteAll<PopEnglish>();
                    }
                    else if (MainPage.gameMode == "The80")
                    {
                        AllPasswords.connection.DeleteAll<The80>();
                        AllPasswords.connectionRestart.DeleteAll<The80>();
                    }
                    else if (MainPage.gameMode == "The80English")
                    {
                        AllPasswords.connection.DeleteAll<The80English>();
                        AllPasswords.connectionRestart.DeleteAll<The80English>();
                    }
                    else if (MainPage.gameMode == "The80Polish")
                    {
                        AllPasswords.connection.DeleteAll<The80Polish>();
                        AllPasswords.connectionRestart.DeleteAll<The80Polish>();
                    }
                    else if (MainPage.gameMode == "RockEnglish")
                    {
                        AllPasswords.connection.DeleteAll<RockEnglish>();
                        AllPasswords.connectionRestart.DeleteAll<RockEnglish>();
                    }
                    else if (MainPage.gameMode == "RockPolish")
                    {
                        AllPasswords.connection.DeleteAll<RockPolish>();
                        AllPasswords.connectionRestart.DeleteAll<RockPolish>();
                    }
                    else if (MainPage.gameMode == "Youtube")
                    {
                        AllPasswords.connection.DeleteAll<Youtube>();
                        AllPasswords.connectionRestart.DeleteAll<Youtube>();
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
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "FairyTales")
                                    {
                                        var songsData = new FairyTales
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "Pop")
                                    {
                                        var songsData = new Pop
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "Rock")
                                    {
                                        var songsData = new Rock
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "UsersMusic")
                                    {
                                        var songsData = new UsersMusic
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "Rap")
                                    {
                                        var songsData = new Rap
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "RapPolish")
                                    {
                                        var songsData = new RapPolish
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "RapEnglish")
                                    {
                                        var songsData = new RapEnglish
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "PopPolish")
                                    {
                                        var songsData = new PopPolish
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "PopEnglish")
                                    {
                                        var songsData = new PopEnglish
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "The80")
                                    {
                                        var songsData = new The80
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "The80English")
                                    {
                                        var songsData = new The80English
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "The80Polish")
                                    {
                                        var songsData = new The80Polish
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "RockEnglish")
                                    {
                                        var songsData = new RockEnglish
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "RockPolish")
                                    {
                                        var songsData = new RockPolish
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }
                                    else if (MainPage.gameMode == "Youtube")
                                    {
                                        var songsData = new Youtube
                                        {
                                            Title = fields[1],
                                            Prompt = fields[0]
                                        };
                                        AllPasswords.connection.Insert(songsData);
                                        AllPasswords.connectionRestart.Insert(songsData);
                                    }



                                    else
                                    {
                                        await App.Current.MainPage.DisplayAlert("Błąd pliku", "Nieprawidłowy format pliku", "OK");
                                        break;
                                    }
                                }
                            }

                            await Navigation.PushAsync(new AddingNewSongs());

                        }
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Błąd pliku", "Nieprawidłowy format pliku", "OK");
                }

            }

        }


        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;

            if (string.IsNullOrEmpty(searchText))
            {
                switch (MainPage.gameMode)
                {
                    case "AllSongs":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<AllSongs>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "FairyTales":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<FairyTales>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "Pop":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Pop>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "Rock":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Rock>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "UsersMusic":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<UsersMusic>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "Rap":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Rap>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "RapPolish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RapPolish>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "RapEnglish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RapEnglish>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "PopPolish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<PopPolish>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "PopEnglish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<PopEnglish>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "The80":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<The80>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "The80Polish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<The80Polish>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "The80English":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<The80English>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "RockPolish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RockPolish>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "RockEnglish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RockEnglish>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "Youtube":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Youtube>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;
                    case "Carols":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Carols>()
                            .OrderBy(song => song.Prompt)
                            .ThenBy(song => song.Title)
                            .ToList();
                        break;

                    case "ChristmasSongs":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<ChristmasSongs>()
                            .OrderBy(song => song.Prompt)
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
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<AllSongs>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "FairyTales":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<FairyTales>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "Pop":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Pop>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "Rock":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Rock>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "UsersMusic":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<UsersMusic>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "Rap":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Rap>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "RapPolish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RapPolish>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "RapEnglish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RapEnglish>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "PopPolish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<PopPolish>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "PopEnglish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<PopEnglish>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "The80":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<The80>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "The80Polish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<The80Polish>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "The80English":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<The80English>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "RockPolish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RockPolish>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "RockEnglish":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<RockEnglish>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "Youtube":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Youtube>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                    case "Carols":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<Carols>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;

                    case "ChristmasSongs":
                        SongsCollection.ItemsSource = AllPasswords.connectionRestart.Table<ChristmasSongs>()
                            .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
                        break;
                }
            }
        }

    }
}