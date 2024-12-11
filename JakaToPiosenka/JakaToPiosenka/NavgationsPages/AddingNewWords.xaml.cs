using JakaToPiosenka.HelpClasses;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JakaToPiosenka.KalamburyClasses;
using JakaToPiosenka.MusicClasses;

namespace JakaToPiosenka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddingNewWords : ContentPage
    {
        Sounds sound = new Sounds();

        // Dictionary dla mapowania tabel na typy
        private static readonly Dictionary<string, Type> TableTypeMap = new Dictionary<string, Type>
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

        public AddingNewWords()
        {
            InitializeComponent();

            // Wczytaj dane na podstawie trybu gry
            LoadSongs(MainPage.gameMode);
        }

        private void LoadSongs(string gameMode)
        {
            if (TableTypeMap.TryGetValue(gameMode, out var tableType))
            {
                try
                {
                    // Get the Table method for the specific type
                    var method = typeof(SQLiteConnection).GetMethod("Table").MakeGenericMethod(tableType);
                    var query = method.Invoke(AllPasswords.connectionRestart, null);
                    var list = ((IEnumerable<object>)query).ToList();

                    // Sort dynamically with null checks
                    var sortedList = list
                        .OrderBy(song => tableType.GetProperty("Prompt")?.GetValue(song)?.ToString() ?? string.Empty)
                        .ThenBy(song => tableType.GetProperty("Title")?.GetValue(song)?.ToString() ?? string.Empty)
                        .ToList();

                    SongsCollection.ItemsSource = sortedList;
                }
                catch (Exception ex)
                {
                    // Handle errors gracefully
                    Console.WriteLine($"Error loading songs: {ex.Message}");
                    SongsCollection.ItemsSource = null; // Clear the collection if an error occurs
                }
            }
            else
            {
                SongsCollection.ItemsSource = null; // Clear the collection if gameMode is invalid
            }
        }



        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new BeforeGameKalambury());
            });
            return true;
        }

        private async void AddSongToList_Clicked(object sender, EventArgs e)
        {
            sound.ClickSound();

            if (!string.IsNullOrWhiteSpace(NewTitleName.Text) && !string.IsNullOrWhiteSpace(NewPromptName.Text))
            {
                if (TableTypeMap.TryGetValue(MainPage.gameMode, out var tableType))
                {
                    var songData = Activator.CreateInstance(tableType);
                    tableType.GetProperty("Title").SetValue(songData, NewPromptName.Text);
                    tableType.GetProperty("Prompt").SetValue(songData, NewTitleName.Text);

                    AllPasswords.connection.Insert(songData);
                    AllPasswords.connectionRestart.Insert(songData);

                    await Navigation.PushAsync(new AddingNewWords());
                    NewTitleName.Text = string.Empty;
                    NewPromptName.Text = string.Empty;
                    LoadSongs(MainPage.gameMode);
                }
                else
                {
                    await DisplayAlert("Błąd", "Nieznany tryb gry!", "OK");
                }
            }
            else
            {
                await DisplayAlert("Błąd", "Proszę podać tytuł i kategorię.", "OK");
            }
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var song = item.CommandParameter;

            if (TableTypeMap.TryGetValue(MainPage.gameMode, out var tableType))
            {
                song = Convert.ChangeType(song, tableType);
                var title = tableType.GetProperty("Title").GetValue(song).ToString();
                var prompt = tableType.GetProperty("Prompt").GetValue(song).ToString();

                bool result = await DisplayAlert("Usuń", $"Czy chcesz usunąć: {title}?", "Tak", "Nie");
                if (result)
                {
                    sound.DeleteSound();
                    string deleteQuery = $"DELETE FROM {MainPage.gameMode} WHERE Title = ? AND Prompt = ?";
                    AllPasswords.connection.Execute(deleteQuery, title, prompt);
                    AllPasswords.connectionRestart.Execute(deleteQuery, title, prompt);

                    LoadSongs(MainPage.gameMode);
                }
            }
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;

            if (TableTypeMap.TryGetValue(MainPage.gameMode, out var tableType))
            {
                try
                {
                    // Use reflection to get the Table<T>() method
                    var method = typeof(SQLiteConnection).GetMethod("Table").MakeGenericMethod(tableType);

                    // Invoke the Table<T>() method dynamically
                    var query = method.Invoke(AllPasswords.connectionRestart, null);

                    // Cast the result to IEnumerable<object> for processing
                    var list = ((IEnumerable<object>)query).ToList();

                    // Filter and sort dynamically
                    var filteredList = list;

                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        filteredList = list.Where(song =>
                            (tableType.GetProperty("Prompt")?.GetValue(song)?.ToString()?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0 ||
                            (tableType.GetProperty("Title")?.GetValue(song)?.ToString()?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0)
                        .ToList();
                    }

                    filteredList = filteredList
                        .OrderBy(song => tableType.GetProperty("Prompt")?.GetValue(song)?.ToString())
                        .ThenBy(song => tableType.GetProperty("Title")?.GetValue(song)?.ToString())
                        .ToList();


                    // Assign the filtered list to the CollectionView
                    SongsCollection.ItemsSource = filteredList;
                }
                catch (Exception ex)
                {
                    // Handle errors gracefully
                    Console.WriteLine($"Error searching songs: {ex.Message}");
                    SongsCollection.ItemsSource = null;
                }
            }
            else
            {
                SongsCollection.ItemsSource = null; // Clear the collection if gameMode is invalid
            }
        }



        //private async void ExportMethod(List<string> promptsList, List<string> titlesList)
        //{
        //    try
        //    {
        //        var result = await DisplayPromptAsync("Eksport", "Wpisz nazwę pliku:", "OK", "Anuluj", placeholder: "Wprowadź tekst");
        //        if (!string.IsNullOrWhiteSpace(result))
        //        {
        //            var fileName = $"{result}_JakaToPiosenka.txt";
        //            var documentsPath = "/storage/emulated/0/Documents";
        //            var filePath = Path.Combine(documentsPath, fileName);

        //            using (var writer = new StreamWriter(filePath))
        //            {
        //                for (int i = 0; i < promptsList.Count; i++)
        //                {
        //                    await writer.WriteLineAsync($"{promptsList[i]};{titlesList[i]}");
        //                }
        //            }

        //            await DisplayAlert("Eksport zakończony", $"Plik zapisano jako {fileName}", "OK");

        //            await Share.RequestAsync(new ShareFileRequest
        //            {
        //                Title = "Eksportuj",
        //                File = new ShareFile(filePath)
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Błąd eksportu", ex.Message, "OK");
        //    }
        //}

        //private async void ImportMethod()
        //{
        //    try
        //    {
        //        // Request permissions to read storage
        //        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        //        if (status != PermissionStatus.Granted)
        //        {
        //            status = await Permissions.RequestAsync<Permissions.StorageRead>();
        //        }

        //        if (status == PermissionStatus.Granted)
        //        {
        //            // Open file picker to select the file
        //            var file = await FilePicker.PickAsync(new PickOptions
        //            {
        //                PickerTitle = "Select a text file",
        //                FileTypes = FilePickerFileType.PlainText
        //            });

        //            if (file != null && file.FileName.EndsWith(".txt"))
        //            {
        //                // Clear existing records for the current game mode
        //                if (TableTypeMap.TryGetValue(MainPage.gameMode, out var tableType))
        //                {
        //                    AllPasswords.connection.DeleteAll(tableType);
        //                    AllPasswords.connectionRestart.DeleteAll(tableType);

        //                    using (var stream = await file.OpenReadAsync())
        //                    using (var reader = new StreamReader(stream))
        //                    {
        //                        string line;
        //                        while ((line = await reader.ReadLineAsync()) != null)
        //                        {
        //                            var fields = line.Split(';');
        //                            if (fields.Length == 2)
        //                            {
        //                                // Dynamically create and populate a new object for the current table
        //                                var newItem = Activator.CreateInstance(tableType);
        //                                tableType.GetProperty("Prompt")?.SetValue(newItem, fields[0]);
        //                                tableType.GetProperty("Title")?.SetValue(newItem, fields[1]);

        //                                // Insert into both databases
        //                                AllPasswords.connection.Insert(newItem);
        //                                AllPasswords.connectionRestart.Insert(newItem);
        //                            }
        //                        }
        //                    }

        //                    // Reload the collection after import
        //                    LoadSongs(MainPage.gameMode);
        //                    await DisplayAlert("Success", "Import completed successfully.", "OK");
        //                }
        //                else
        //                {
        //                    await DisplayAlert("Error", "Invalid game mode. Cannot import data.", "OK");
        //                }
        //            }
        //            else
        //            {
        //                await DisplayAlert("Error", "Invalid file format. Please select a .txt file.", "OK");
        //            }
        //        }
        //        else
        //        {
        //            await DisplayAlert("Permission Denied", "Storage access is required to import files.", "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Error", $"An error occurred during import: {ex.Message}", "OK");
        //    }
        //}



    }
}