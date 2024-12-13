using Xamarin.Forms;
using JakaToPiosenka.HelpClasses;
using System;

namespace JakaToPiosenka
{
    public partial class SettingsPage : ContentPage
    {


        private readonly Sounds _sounds = new Sounds();
        // Statyczne pola
        public static int Time1;
        public static int Time2;
        public static int Time3;
        public static int Time4;
        public static int WordsNumber;
        public static bool isMuted;

        public SettingsPage()
        {
            InitializeComponent();

            // Inicjalizacja ustawień
            InitializeSettings();

            // Ustawienie wartości w polach
            Time1Entry.Text = Time1.ToString();
            Time2Entry.Text = Time2.ToString();
            Time3Entry.Text = Time3.ToString();
            Time4Entry.Text = Time4.ToString();
            WordsEntry.Text = WordsNumber.ToString();
        }

        public void InitializeSettings()
        {
            Time1 = SettingsHelper.GetValue("Time1", 15);
            Time2 = SettingsHelper.GetValue("Time2", 30);
            Time3 = SettingsHelper.GetValue("Time3", 45);
            Time4 = SettingsHelper.GetValue("Time4", 60);
            WordsNumber = SettingsHelper.GetValue("WordsNumber", 10);
            isMuted = MuteHelper.GetMuteState();
            UpdateMuteButton();

        }

        private void SaveSetting(string key, int value)
        {
            SettingsHelper.SetValue(key, value);
        }

        // Timer 1 Handlers
        private void OnIncreaseTime1(object sender, EventArgs e)
        {
            Time1++;
            Time1Entry.Text = Time1.ToString();
            SaveSetting("Time1", Time1);
            _sounds.ClickSound();
        }

        private void OnDecreaseTime1(object sender, EventArgs e)
        {
            if (Time1 > 1)
            {
                Time1--;
                Time1Entry.Text = Time1.ToString();
                SaveSetting("Time1", Time1);
                _sounds.ClickSound();
            }
        }

        // Timer 2 Handlers
        private void OnIncreaseTime2(object sender, EventArgs e)
        {
            Time2++;
            Time2Entry.Text = Time2.ToString();
            SaveSetting("Time2", Time2);
            _sounds.ClickSound();
        }

        private void OnDecreaseTime2(object sender, EventArgs e)
        {
            if (Time2 > 1)
            {
                Time2--;
                Time2Entry.Text = Time2.ToString();
                SaveSetting("Time2", Time2);
                _sounds.ClickSound();
            }
        }

        // Timer 3 Handlers
        private void OnIncreaseTime3(object sender, EventArgs e)
        {
            Time3++;
            Time3Entry.Text = Time3.ToString();
            SaveSetting("Time3", Time3);
            _sounds.ClickSound();
        }

        private void OnDecreaseTime3(object sender, EventArgs e)
        {
            if (Time3 > 1)
            {
                Time3--;
                Time3Entry.Text = Time3.ToString();
                SaveSetting("Time3", Time3);
                _sounds.ClickSound();
            }
        }

        // Timer 4 Handlers
        private void OnIncreaseTime4(object sender, EventArgs e)
        {
            Time4++;
            Time4Entry.Text = Time4.ToString();
            SaveSetting("Time4", Time4);
            _sounds.ClickSound();
        }

        private void OnDecreaseTime4(object sender, EventArgs e)
        {
            if (Time4 > 1)
            {
                Time4--;
                Time4Entry.Text = Time4.ToString();
                SaveSetting("Time4", Time4);
                _sounds.ClickSound();
            }
        }

        // Words Number Handlers
        private void WordsIncrease_Clicked(object sender, EventArgs e)
        {
            WordsNumber++;
            WordsEntry.Text = WordsNumber.ToString();
            SaveSetting("WordsNumber", WordsNumber);
            _sounds.ClickSound();
        }

        private void WordsDecrease_Clicked(object sender, EventArgs e)
        {
            if (WordsNumber > 1)
            {
                WordsNumber--;
                WordsEntry.Text = WordsNumber.ToString();
                SaveSetting("WordsNumber", WordsNumber);
                _sounds.ClickSound();
            }
        }

        private void WordsEntry_Completed(object sender, EventArgs e)
        {
            ValidateWordsNumber();
        }

        private void ValidateWordsNumber()
        {
            if (int.TryParse(WordsEntry.Text, out int words) && words > 0)
            {
                WordsNumber = words;
                SaveSetting("WordsNumber", WordsNumber);
            }
            else
            {
                DisplayAlert("Błąd", "Ilość haseł musi być liczbą większą od 0.", "OK");
                WordsEntry.Text = WordsNumber.ToString();
            }
        }

        private void OnTimeCompleted1(object sender, EventArgs e) => ValidateTime(Time1Entry, "Time1", ref Time1);
        private void OnTimeCompleted2(object sender, EventArgs e) => ValidateTime(Time2Entry, "Time2", ref Time2);
        private void OnTimeCompleted3(object sender, EventArgs e) => ValidateTime(Time3Entry, "Time3", ref Time3);
        private void OnTimeCompleted4(object sender, EventArgs e) => ValidateTime(Time4Entry, "Time4", ref Time4);

        private void ValidateTime(Entry entry, string key, ref int timeValue)
        {
            if (int.TryParse(entry.Text, out int time) && time > 0)
            {
                timeValue = time;
                SaveSetting(key, timeValue);
            }
            else
            {
                DisplayAlert("Błąd", "Czas musi być liczbą większą od 0.", "OK");
                entry.Text = timeValue.ToString();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            InitializeSettings();
            if (MainPage.isMainPage)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new MainPage());
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new KalamburyPage());
                });
            }

            return true;
        }



        private void MuteButton_Clicked(object sender, EventArgs e)
        {
            _sounds.ClickSound();
            // Toggle the mute state
            isMuted = !isMuted;

            // Save the new state to the database
            MuteHelper.SetMuteState(isMuted);

            // Update the button appearance
            UpdateMuteButton();
        }

        private void UpdateMuteButton()
        {
            MuteButton.Source = isMuted ? "mute.png" : "unmute.png";
        }
    }
}