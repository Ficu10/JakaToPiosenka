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
        public static int[] goodBadSongs = new int[10];

        public static List<string> songsFromGame = new List<string>();
        public Game()
        {
            InitializeComponent();
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            ShowGame();
        }

        public void Dispose()
        {
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
                    sounds.WinningSound();
                    BackgroundImageSource = "green.jpg";
                    SongTitle.Text = "Dobrze";
                    Time.IsVisible = false;
                    TitlePrompt.IsVisible = false;
                    WrongAnswearButton.IsEnabled = false;
                    endOfQuestion = true;
                    goodAnswer = true;
                    answered = true;
                }
                else if (z < -0.6 && x < 0.9)
                {
                    sounds.LosingSound();
                    BackgroundImageSource = "red.jpg";
                    SongTitle.Text = "Brak odpowiedzi";
                    Time.IsVisible = false;
                    TitlePrompt.IsVisible = false;
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
            return true;
        }
        void GameType()
        {

            if (MainPage.gameMode == "AllSongs")
            {
                StartGame(AllPasswords.connection.Table<AllSongs>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<AllSongs>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<AllSongs>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<AllSongs>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "FairyTales")
            {
                StartGame(AllPasswords.connection.Table<FairyTales>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<FairyTales>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<FairyTales>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<FairyTales>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Pop")
            {
                StartGame(AllPasswords.connection.Table<Pop>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Pop>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Pop>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Pop>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Rock")
            {
                StartGame(AllPasswords.connection.Table<Rock>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Rock>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Rock>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Rock>().ToList().Select(x => x.Title).ToList());

            }
            else if (MainPage.gameMode == "UsersMusic")
            {
                StartGame(AllPasswords.connection.Table<UsersMusic>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<UsersMusic>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<UsersMusic>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<UsersMusic>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Rap")
            {
                StartGame(AllPasswords.connection.Table<Rap>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Rap>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Rap>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Rap>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RapPolish")
            {
                StartGame(AllPasswords.connection.Table<RapPolish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<RapPolish>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<RapPolish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<RapPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RapEnglish")
            {
                StartGame(AllPasswords.connection.Table<RapEnglish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<RapEnglish>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<RapEnglish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<RapEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "PopPolish")
            {
                StartGame(AllPasswords.connection.Table<PopPolish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<PopPolish>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<PopPolish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<PopPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "PopEnglish")
            {
                StartGame(AllPasswords.connection.Table<PopEnglish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<PopEnglish>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<PopEnglish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<PopEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80")
            {
                StartGame(AllPasswords.connection.Table<The80>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<The80>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<The80>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<The80>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80English")
            {
                StartGame(AllPasswords.connection.Table<The80English>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<The80English>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<The80English>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<The80English>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80Polish")
            {
                StartGame(AllPasswords.connection.Table<The80Polish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<The80Polish>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<The80Polish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<The80Polish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RockEnglish")
            {
                StartGame(AllPasswords.connection.Table<RockEnglish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<RockEnglish>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<RockEnglish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<RockEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RockPolish")
            {
                StartGame(AllPasswords.connection.Table<RockPolish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<RockPolish>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<RockPolish>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<RockPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Youtube")
            {
                StartGame(AllPasswords.connection.Table<Youtube>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Youtube>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Youtube>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Youtube>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Children")
            {
                StartGame(AllPasswords.connection.Table<Children>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Children>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Children>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Children>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Countries")
            {
                StartGame(AllPasswords.connection.Table<Countries>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Countries>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Countries>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Countries>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Emotions")
            {
                StartGame(AllPasswords.connection.Table<Emotions>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Emotions>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Emotions>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Emotions>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "FictionalCharacter")
            {
                StartGame(AllPasswords.connection.Table<FictionalCharacter>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<FictionalCharacter>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<FictionalCharacter>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<FictionalCharacter>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "HistoricalCharacter")
            {
                StartGame(AllPasswords.connection.Table<HistoricalCharacter>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<HistoricalCharacter>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<HistoricalCharacter>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<HistoricalCharacter>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Jobs")
            {
                StartGame(AllPasswords.connection.Table<Jobs>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Jobs>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Jobs>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Jobs>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Movies")
            {
                StartGame(AllPasswords.connection.Table<Movies>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Movies>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Movies>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Movies>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Series")
            {
                StartGame(AllPasswords.connection.Table<Series>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Series>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Series>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Series>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Tales")
            {
                StartGame(AllPasswords.connection.Table<Tales>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Tales>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Tales>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Tales>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Words")
            {
                StartGame(AllPasswords.connection.Table<Words>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Words>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Words>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Words>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Carols")
            {
                StartGame(AllPasswords.connection.Table<Carols>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Carols>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Carols>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Carols>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "ChristmasSongs")
            {
                StartGame(AllPasswords.connection.Table<ChristmasSongs>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<ChristmasSongs>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<ChristmasSongs>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<ChristmasSongs>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Animals")
            {
                StartGame(AllPasswords.connection.Table<Animals>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Animals>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Animals>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Animals>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "AdultMixed")
            {
                StartGame(AllPasswords.connection.Table<AdultMixed>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<AdultMixed>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<AdultMixed>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<AdultMixed>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Celebrities")
            {
                StartGame(AllPasswords.connection.Table<Celebrities>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Celebrities>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Celebrities>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Celebrities>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "DailyLife")
            {
                StartGame(AllPasswords.connection.Table<DailyLife>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<DailyLife>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<DailyLife>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<DailyLife>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Poland")
            {
                StartGame(AllPasswords.connection.Table<Poland>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Poland>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Poland>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Poland>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Rhymes")
            {
                StartGame(AllPasswords.connection.Table<Rhymes>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Rhymes>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Rhymes>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Rhymes>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "ScienceTopics")
            {
                StartGame(AllPasswords.connection.Table<ScienceTopics>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<ScienceTopics>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<ScienceTopics>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<ScienceTopics>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Sports")
            {
                StartGame(AllPasswords.connection.Table<Sports>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connection.Table<Sports>().ToList().Select(x => x.Title).ToList(), AllPasswords.connectionRestart.Table<Sports>().ToList().Select(x => x.Prompt).ToList(), AllPasswords.connectionRestart.Table<Sports>().ToList().Select(x => x.Title).ToList());
            }


        }
        public void ShowGame()
        {

            TitlePrompt.IsVisible = true;
            Time.IsVisible = true;
            SongTitle.IsVisible = true;
            Task modifyTaskOne = Task.Run(() => GameType());
        }

        public void StartGame(List<string> PromptsList, List<string> songsList, List<string> PromptsListReset, List<string> songsListReset)
        {
            sounds.CountdownSound();
            answered = true;
            Thread.Sleep(3000);
            answered = false;
            songsFromGame.Clear();
            int gameCounter = 10;
            Random r = new Random();

            while (gameCounter > 0)
            {
                if (PromptsList.Count < 1)
                {
                    for (int i = 0; i < PromptsListReset.Count; i++)
                    {
                        PromptsList.Add(PromptsListReset[i]);
                        songsList.Add(songsListReset[i]);
                    }


                }
                newGame = true;
                songId = r.Next(PromptsList.Count);


                seconds = BeforeGamePage.timeChanger + 1;
                while (newGame == true)
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
                            sounds.LosingSound();
                            endOfQuestion = true;
                        }
                        if (endOfQuestion == true)
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
                            string titleToRemove = songsList[songId];
                            string PromptToRemove = PromptsList[songId];


                            string deleteQuery = $"DELETE FROM {MainPage.gameMode} WHERE Title = ? AND Prompt = ?";
                            AllPasswords.connection.Execute(deleteQuery, titleToRemove, PromptToRemove);
                            PromptsList.RemoveAt(songId);
                            songsList.RemoveAt(songId);
                            if (goodAnswer == true)
                            {
                                endOfQuestion = false;
                                pointsCounter++;
                                goodAnswer = false;
                                BackgroundImageSource = "green.jpg";
                                SongTitle.Text = "Dobrze";
                                goodBadSongs[goodBadCounter] = 1; 
                            }
                            else
                            {
                                endOfQuestion = false;
                                BackgroundImageSource = "red.jpg";
                                SongTitle.Text = "Brak odpowiedzi";
                                goodBadSongs[goodBadCounter] = 0;
                            }
                            goodBadCounter++;
                            if (gameCounter == 0)
                            {
                                Dispose();
                                Accelerometer.Stop();
                                if (MainPage.isMainPage)
                                {
                                    await Navigation.PushAsync(new AfterGame());
                                }
                                else
                                {
                                    await Navigation.PushAsync(new AfterGameKalambury());
                                }
                            }
                        }

                    });
                    Thread.Sleep(1000);


                }
            }



        }
        private void WrongAnswearButton_Clicked(object sender, EventArgs e)
        {
            if (answered == false)
            {
                sounds.LosingSound();
                BackgroundImageSource = "red.jpg";
                SongTitle.Text = "Brak odpowiedzi";
                Time.IsVisible = false;
                TitlePrompt.IsVisible = false;
                WrongAnswearButton.IsEnabled = false;
                endOfQuestion = true;
            }

        }

    }
}