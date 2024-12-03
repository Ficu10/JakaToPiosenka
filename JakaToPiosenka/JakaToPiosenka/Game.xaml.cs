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
        public static SongsAndAuthors endList = new SongsAndAuthors();
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
                    SongAuthor.IsVisible = false;
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
                    SongAuthor.IsVisible = false;
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
                StartGame(MUSICTYPES.connection.Table<AllSongs>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<AllSongs>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<AllSongs>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<AllSongs>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "FairyTales")
            {
                StartGame(MUSICTYPES.connection.Table<FairyTales>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<FairyTales>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<FairyTales>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<FairyTales>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Pop")
            {
                StartGame(MUSICTYPES.connection.Table<Pop>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<Pop>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<Pop>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<Pop>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Rock")
            {
                StartGame(MUSICTYPES.connection.Table<Rock>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<Rock>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<Rock>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<Rock>().ToList().Select(x => x.Title).ToList());

            }
            else if (MainPage.gameMode == "UsersMusic")
            {
                StartGame(MUSICTYPES.connection.Table<UsersMusic>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<UsersMusic>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<UsersMusic>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<UsersMusic>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Rap")
            {
                StartGame(MUSICTYPES.connection.Table<Rap>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<Rap>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<Rap>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<Rap>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RapPolish")
            {
                StartGame(MUSICTYPES.connection.Table<RapPolish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<RapPolish>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<RapPolish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<RapPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RapEnglish")
            {
                StartGame(MUSICTYPES.connection.Table<RapEnglish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<RapEnglish>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<RapEnglish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<RapEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "PopPolish")
            {
                StartGame(MUSICTYPES.connection.Table<PopPolish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<PopPolish>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<PopPolish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<PopPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "PopEnglish")
            {
                StartGame(MUSICTYPES.connection.Table<PopEnglish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<PopEnglish>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<PopEnglish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<PopEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80")
            {
                StartGame(MUSICTYPES.connection.Table<The80>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<The80>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<The80>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<The80>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80English")
            {
                StartGame(MUSICTYPES.connection.Table<The80English>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<The80English>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<The80English>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<The80English>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "The80Polish")
            {
                StartGame(MUSICTYPES.connection.Table<The80Polish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<The80Polish>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<The80Polish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<The80Polish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RockEnglish")
            {
                StartGame(MUSICTYPES.connection.Table<RockEnglish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<RockEnglish>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<RockEnglish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<RockEnglish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "RockPolish")
            {
                StartGame(MUSICTYPES.connection.Table<RockPolish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<RockPolish>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<RockPolish>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<RockPolish>().ToList().Select(x => x.Title).ToList());
            }
            else if (MainPage.gameMode == "Youtube")
            {
                StartGame(MUSICTYPES.connection.Table<Youtube>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connection.Table<Youtube>().ToList().Select(x => x.Title).ToList(), MUSICTYPES.connectionRestart.Table<Youtube>().ToList().Select(x => x.Author).ToList(), MUSICTYPES.connectionRestart.Table<Youtube>().ToList().Select(x => x.Title).ToList());
            }
        }
        public void ShowGame()
        {
            SongAuthor.IsVisible = true;
            Time.IsVisible = true;
            SongTitle.IsVisible = true;
            Task modifyTaskOne = Task.Run(() => GameType());
        }

        public void StartGame(List<string> authorsList, List<string> songsList, List<string> authorsListReset, List<string> songsListReset)
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
                if (authorsList.Count < 1)
                {
                    for (int i = 0; i < authorsListReset.Count; i++)
                    {
                        authorsList.Add(authorsListReset[i]);
                        songsList.Add(songsListReset[i]);
                    }


                }
                newGame = true;
                songId = r.Next(authorsList.Count);


                seconds = BeforeGamePage.timeChanger + 1;
                while (newGame == true)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        WrongAnswearButton.IsEnabled = true;
                        SongAuthor.IsVisible = true;
                        Time.IsVisible = true;
                        BackgroundImageSource = "blue.jpg";
                        SongAuthor.Text = authorsList[songId];
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

                            SongAuthor.IsVisible = false;
                            Time.IsVisible = false;
                            newGame = false;
                            songsFromGame.Add(songsList[songId]);
                            endList = new SongsAndAuthors
                            {
                                Title = songsList[songId],
                                Author = authorsList[songId]
                            };
                            gameCounter--;
                            string titleToRemove = songsList[songId];
                            string authorToRemove = authorsList[songId];


                            string deleteQuery = $"DELETE FROM {MainPage.gameMode} WHERE Title = ? AND Author = ?";
                            MUSICTYPES.connection.Execute(deleteQuery, titleToRemove, authorToRemove);
                            authorsList.RemoveAt(songId);
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
                                await Navigation.PushAsync(new AfterGame());
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
                SongAuthor.IsVisible = false;
                WrongAnswearButton.IsEnabled = false;
                endOfQuestion = true;
            }

        }

    }
}