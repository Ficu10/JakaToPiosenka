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
using System.Reflection;
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

            // Sprawdzenie, czy istnieje odpowiedni typ dla bieżącego trybu gry
            if (NamesTable.namesTable.TryGetValue(MainPage.gameMode, out var tableType))
            {
                // Pobranie danych dynamicznie z odpowiedniej tabeli
                var items = AllPasswords.connectionRestart.GetType()
                    .GetMethod("Table")?
                    .MakeGenericMethod(tableType)
                    .Invoke(AllPasswords.connectionRestart, null) as IEnumerable<dynamic>;

                if (items != null)
                {
                    // Posortowanie danych i przypisanie do ItemsSource
                    SongsCollection.ItemsSource = items
                        .OrderBy(song => song.Prompt)
                        .ThenBy(song => song.Title)
                        .ToList();
                }
            }
            else
            {
                // Obsługa przypadku, gdy tryb gry nie istnieje
                SongsCollection.ItemsSource = null;
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
              

                if (NamesTable.namesTable.ContainsKey(MainPage.gameMode))
                {
                    var tableType = NamesTable.namesTable[MainPage.gameMode];
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
        }

        private void Eksport_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();

            // Sprawdzenie, czy tryb gry istnieje w słowniku
            if (NamesTable.namesTable.TryGetValue(MainPage.gameMode, out var tableType))
            {
                // Pobranie danych z tabeli dynamicznie na podstawie typu
                var tableData = AllPasswords.connectionRestart.GetType()
                    .GetMethod("Table")?
                    .MakeGenericMethod(tableType)
                    .Invoke(AllPasswords.connectionRestart, null) as IEnumerable<dynamic>;

                if (tableData != null)
                {
                    // Przygotowanie list dla metody eksportu z jawnych rzutowaniem na string
                    var prompts = tableData.Select(x => (string)x.Prompt).ToList();
                    var titles = tableData.Select(x => (string)x.Title).ToList();

                    // Wywołanie metody eksportu
                    ExportMethod(prompts, titles);
                }
            }
            else
            {
                // Obsługa przypadku, gdy tryb gry nie istnieje
                throw new InvalidOperationException($"Invalid game mode: {MainPage.gameMode}");
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
                    string fileName = result + "Zgadula.txt";


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
            try
            {
                // Wybór pliku
                var file = await FilePicker.PickAsync();
                if (file == null || !file.FileName.EndsWith(".txt"))
                {
                    await App.Current.MainPage.DisplayAlert("Błąd pliku", "Nieprawidłowy format pliku", "OK");
                    return;
                }


                if (!NamesTable.namesTable.TryGetValue(MainPage.gameMode, out var tableType))
                {
                    await App.Current.MainPage.DisplayAlert("Błąd", "Nieznany tryb gry", "OK");
                    return;
                }

                // Usunięcie istniejących danych
                var clearTableMethod = typeof(AllPasswords).GetMethod("ClearTable")?.MakeGenericMethod(tableType);
                clearTableMethod?.Invoke(null, null);

                // Importowanie danych z pliku
                using (Stream stream = await file.OpenReadAsync())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = line.Split(';');
                        if (fields.Length == 2)
                        {
                            // Tworzenie nowego obiektu i ustawianie danych
                            var instance = Activator.CreateInstance(tableType) as AllPasswords;
                            if (instance != null)
                            {
                                instance.Prompt = fields[0];
                                instance.Title = fields[1];

                                // Wstawianie danych do bazy
                                var insertDataMethod = typeof(AllPasswords).GetMethod("InsertData")?.MakeGenericMethod(tableType);
                                insertDataMethod?.Invoke(null, new object[] { instance });
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Błąd pliku", "Nieprawidłowy format pliku", "OK");
                            return;
                        }
                    }
                }

                await Navigation.PushAsync(new AddingNewSongs());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił problem podczas importu: {ex.Message}", "OK");
            }
        }


        // Usuwanie wszystkich danych z tabeli
        private void DeleteAllData(Type tableType)
        {
            // Znajdź metodę CreateTable z odpowiednią sygnaturą
            var createTableMethod = AllPasswords.connection.GetType()
                .GetMethod("CreateTable", BindingFlags.Public | BindingFlags.Instance)
                ?.MakeGenericMethod(tableType);

            createTableMethod?.Invoke(AllPasswords.connection, null);

            var createTableRestartMethod = AllPasswords.connectionRestart.GetType()
                .GetMethod("CreateTable", BindingFlags.Public | BindingFlags.Instance)
                ?.MakeGenericMethod(tableType);

            createTableRestartMethod?.Invoke(AllPasswords.connectionRestart, null);

            // Znajdź metodę DeleteAll z odpowiednią sygnaturą
            var deleteAllMethod = AllPasswords.connection.GetType()
                .GetMethod("DeleteAll", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)
                ?.MakeGenericMethod(tableType);

            deleteAllMethod?.Invoke(AllPasswords.connection, null);

            var deleteAllRestartMethod = AllPasswords.connectionRestart.GetType()
                .GetMethod("DeleteAll", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)
                ?.MakeGenericMethod(tableType);

            deleteAllRestartMethod?.Invoke(AllPasswords.connectionRestart, null);
        }

        // Wstawianie danych do tabeli
        private void InsertData(Type tableType, string prompt, string title)
        {
            var instance = Activator.CreateInstance(tableType) as AllPasswords;
            if (instance != null)
            {
                instance.Prompt = prompt;
                instance.Title = title;

                AllPasswords.connection.Insert(instance);
                AllPasswords.connectionRestart.Insert(instance);
            }
        }




        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;
            // Sprawdzenie, czy istnieje odpowiedni typ dla bieżącego trybu gry
            if (NamesTable.namesTable.TryGetValue(MainPage.gameMode, out var tableType))
            {
                // Pobranie danych dynamicznie z odpowiedniej tabeli i ustawienie źródła
                var items = AllPasswords.connectionRestart.GetType()
                    .GetMethod("Table")?
                    .MakeGenericMethod(tableType)
                    .Invoke(AllPasswords.connectionRestart, null) as IEnumerable<dynamic>;

                if (items != null)
                {
                    SongsCollection.ItemsSource = items
                       .Where(song => song.Prompt.ToLower().Contains(searchText.ToLower()) || song.Title.ToLower().Contains(searchText.ToLower())).ToList();
                }
            }
            else
            {
                // Obsługa przypadku, gdy tryb gry nie istnieje
                SongsCollection.ItemsSource = null;
            }
        }

    }
}