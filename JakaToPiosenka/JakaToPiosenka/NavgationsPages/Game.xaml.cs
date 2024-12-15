using JakaToPiosenka.HelpClasses;
using JakaToPiosenka.KalamburyClasses;
using JakaToPiosenka.MusicClasses;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace JakaToPiosenka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Game : ContentPage
    {

        int songId;
        public static int pointsCounter;
        bool newGame = true;
        int seconds;
        bool endOfQuestion = false;
        bool goodAnswer = false;
        bool answered = true;
        Sounds sounds = new Sounds();
        public static AllData endList = new AllData();
        int goodBadCounter = 0;
        public static int[] goodBadSongs = new int[SettingsPage.WordsNumber];
        int gameCounter = SettingsPage.WordsNumber;


        public static List<string> songsFromGame = new List<string>();
        public Game()
        {
            InitializeComponent();
            goodBadSongs = new int[SettingsPage.WordsNumber];
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            ShowGame();
        }

        public void AccStart()
        {
            Accelerometer.Start(SensorSpeed.Game);
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }
        public void Dispose()
        {
            Accelerometer.Stop();
            Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
        }
        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var acceleration = e.Reading.Acceleration;
            var x = acceleration.X;
            var y = acceleration.Y;
            var z = acceleration.Z;

            // dobrze x < 0.6 z > 0.6    zle z<0.6  x<0.9

            if (answered == false)
            {
                if (x <= 0.6 && y < 0.2 && z > 0.6)
                {
                    WrongAnswearButton.IsEnabled = false;
                    endOfQuestion = true;
                    goodAnswer = true;
                    answered = true;
                }
                else if (z < -0.6 && x < 0.9)
                {
                    WrongAnswearButton.IsEnabled = false;
                    endOfQuestion = true;
                    answered = true;
                }

            }
            if (x >= 1 && y < 0.2 && z < 0.2)
            {
                answered = false;
            }

        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool exitGame = await DisplayAlert("Zakończyć grę?", "Czy na pewno chcesz zakończyć grę? Wszystkie aktualne wyniki zostaną zresetowane.", "Tak", "Nie");

                if (exitGame)
                {
                    // Zatrzymaj grę i wyczyść wyniki
                    Accelerometer.Stop();
                    ResetGameState();

                    // Przekieruj do BeforeGamePage
                    if (MainPage.isMainPage)
                    {
                        await Navigation.PushAsync(new BeforeGamePage());
                    }
                    else
                    {
                        await Navigation.PushAsync(new BeforeGameKalambury());
                    }
                    sounds.StopAllSounds();
                }
            });

            // Zablokuj domyślne zamykanie strony
            return true;
        }

        private void ResetGameState()
        {
            // Resetuj wyniki
            pointsCounter = 0;
            goodBadCounter = 0;
            goodBadSongs = new int[SettingsPage.WordsNumber];
            songsFromGame.Clear();

            // Resetuj inne flagi gry
            newGame = false;
            endOfQuestion = false;
            goodAnswer = false;
            answered = true;
            gameCounter = -1;
        }


        void GameType()
        {
            try
            {
                // Pobierz typ tabeli na podstawie trybu gry
                if (NamesTable.namesTable.TryGetValue(MainPage.gameMode, out var tableType))
                {
                    // Pobierz dane z tabel głównej i restartowej
                    var mainPrompts = LoadDataFromTable(tableType, AllPasswords.connection, "Prompt");
                    var mainTitles = LoadDataFromTable(tableType, AllPasswords.connection, "Title");

                    var restartPrompts = LoadDataFromTable(tableType, AllPasswords.connectionRestart, "Prompt");
                    var restartTitles = LoadDataFromTable(tableType, AllPasswords.connectionRestart, "Title");

                    // Uruchom grę
                    StartGame(mainPrompts, mainTitles, restartPrompts, restartTitles);
                }
                else
                {
                    Console.WriteLine($"Nieznany tryb gry: {MainPage.gameMode}");
                    throw new InvalidOperationException($"Nieznany tryb gry: {MainPage.gameMode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd w StartGameType: {ex.Message}");
            }
        }

        private List<string> LoadDataFromTable(Type tableType, SQLiteConnection connection, string columnName)
        {
            try
            {
                // Pobierz nazwę tabeli z mapowania
                var tableMapping = connection.GetMapping(tableType);

                // Przygotuj zapytanie SQL
                var query = $"SELECT {columnName} FROM {tableMapping.TableName}";

                // Wykonaj zapytanie i zwróć wyniki
                return connection.QueryScalars<string>(query).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas ładowania danych: {ex.Message}");
                return new List<string>();
            }
        }

        public void ShowGame()
        {

            TitlePrompt.IsVisible = true;
            Time.IsVisible = true;
            SongTitle.IsVisible = true;
            Task modifyTaskOne = Task.Run(() => GameType());
        }

        public async void StartGame(List<string> PromptsList, List<string> songsList, List<string> PromptsListReset, List<string> songsListReset)
        {
            sounds.CountdownSound();
            Dispose();
            await Task.Delay(3000);
            songsFromGame.Clear();
            Random r = new Random();

            while (gameCounter > 0)
            {
                AccStart();
                // Przywróć dane, jeśli lista jest pusta
                if (PromptsList.Count < 1)
                {
                    if (PromptsListReset.Any() && songsListReset.Any())
                    {
                        PromptsList.AddRange(PromptsListReset);
                        songsList.AddRange(songsListReset);
                    }
                    else
                    {
                        // Wyjdź z pętli, jeśli nie ma więcej danych do załadowania
                        Console.WriteLine("Brak danych do odświeżenia z bazy restart.");
                        break;
                    }
                }

                if (PromptsList.Count == 0)
                {
                    Console.WriteLine("PromptsList jest pusta. Nie można kontynuować gry.");
                    break;
                }

                // Losowanie indeksu
                songId = r.Next(PromptsList.Count);

                newGame = true;
                seconds = BeforeGamePage.timeChanger + 1;

                while (newGame)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        WrongAnswearButton.IsEnabled = true;
                        TitlePrompt.IsVisible = true;
                        Time.IsVisible = true;
                        BackgroundImageSource = "blue.jpg";
                        TitlePrompt.Text = PromptsList[songId];
                        SongTitle.Text = songsList[songId];
                        seconds--;
                        Time.Text = seconds.ToString();

                        if (seconds < 1)
                        {
                            endOfQuestion = true;
                        }

                        if (endOfQuestion)
                        {
                            TitlePrompt.IsVisible = false;
                            Time.IsVisible = false;
                            newGame = false;

                            songsFromGame.Add(songsList[songId]);
                            endList = new AllData
                            {
                                Title = songsList[songId],
                                Prompt = PromptsList[songId]
                            };

                            gameCounter--;

                            // Usuń dane z list
                            if (songId < PromptsList.Count)
                            {
                                PromptsList.RemoveAt(songId);
                                songsList.RemoveAt(songId);
                            }

                            if (goodAnswer)
                            {
                                sounds.WinningSound();
                                endOfQuestion = false;
                                pointsCounter++;
                                goodAnswer = false;
                                BackgroundImageSource = "green.jpg";
                                SongTitle.Text = "Dobrze";
                                goodBadSongs[goodBadCounter] = 1;
                            }
                            else
                            {
                                sounds.LosingSound();
                                endOfQuestion = false;
                                BackgroundImageSource = "red.jpg";
                                SongTitle.Text = "Brak odpowiedzi";
                                goodBadSongs[goodBadCounter] = 0;
                            }

                            goodBadCounter++;

                            if (gameCounter == 0)
                            {
                                await Task.Delay(500);
                                Accelerometer.Stop();
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    if (MainPage.isMainPage)
                                    {
                                        await Navigation.PushAsync(new AfterGame());
                                    }
                                    else
                                    {
                                        await Navigation.PushAsync(new AfterGameKalambury());
                                    }
                                });
                            }
                            Dispose();
                        }
                    });

                    await Task.Delay(500);
                }
            }
        }


        private async void WrongAnswearButton_Clicked(object sender, EventArgs e)
        {
            if (answered == false)
            {
                BackgroundImageSource = "red.jpg";
                SongTitle.Text = "Brak odpowiedzi";
                Time.IsVisible = false;
                TitlePrompt.IsVisible = false;
                WrongAnswearButton.IsEnabled = false;
                endOfQuestion = true;
                Dispose();
            }
            await Task.Delay(500);

        }

    }
}