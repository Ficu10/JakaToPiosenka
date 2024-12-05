using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JakaToPiosenka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public static int Time1 = 15;
        public static int Time2 = 30;
        public static int Time3 = 45;
        public static int Time4 = 60;
        public SettingsPage()
        {
            InitializeComponent();
            Time1Entry.Text = Time1.ToString();
            Time2Entry.Text = Time2.ToString();
            Time3Entry.Text = Time3.ToString();
            Time4Entry.Text = Time4.ToString();
        }
        protected override bool OnBackButtonPressed()
        {
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
        // Timer 1 Handlers
        private void OnIncreaseTime1(object sender, EventArgs e)
        {
            if (int.TryParse(Time1Entry.Text, out int time))
            {
                time++;
                Time1Entry.Text = time.ToString();
                Time1 = int.Parse(Time1Entry.Text);

            }
        }

        private void OnDecreaseTime1(object sender, EventArgs e)
        {
            if (int.TryParse(Time1Entry.Text, out int time) && time > 1)
            {
                time--;
                Time1Entry.Text = time.ToString();
                Time1 = int.Parse(Time1Entry.Text);
            }
        }

        

        // Timer 2 Handlers
        private void OnIncreaseTime2(object sender, EventArgs e)
        {
            if (int.TryParse(Time2Entry.Text, out int time))
            {
                time++;
                Time2Entry.Text = time.ToString();
                Time2 = int.Parse(Time2Entry.Text);
            }
        }

        private void OnDecreaseTime2(object sender, EventArgs e)
        {
            if (int.TryParse(Time2Entry.Text, out int time) && time > 1)
            {
                time--;
                Time2Entry.Text = time.ToString();
                Time2 = int.Parse(Time2Entry.Text);
            }
        }

     

        // Timer 3 Handlers
        private void OnIncreaseTime3(object sender, EventArgs e)
        {
            if (int.TryParse(Time3Entry.Text, out int time))
            {
                time++;
                Time3Entry.Text = time.ToString();
                Time3 = int.Parse(Time3Entry.Text);
            }
        }

        private void OnDecreaseTime3(object sender, EventArgs e)
        {
            if (int.TryParse(Time3Entry.Text, out int time) && time > 1)
            {
                time--;
                Time3Entry.Text = time.ToString();
                Time3 = int.Parse(Time3Entry.Text);
            }
        }

       

        // Timer 4 Handlers
        private void OnIncreaseTime4(object sender, EventArgs e)
        {
            if (int.TryParse(Time4Entry.Text, out int time))
            {
                time++;
                Time4Entry.Text = time.ToString();
                Time4 = int.Parse(Time4Entry.Text);
            }
        }

        private void OnDecreaseTime4(object sender, EventArgs e)
        {
            if (int.TryParse(Time4Entry.Text, out int time) && time > 1)
            {
                time--;
                Time4Entry.Text = time.ToString();
                Time4 = int.Parse(Time4Entry.Text);
            }
        }

        private void OnTimeCompleted1(object sender, EventArgs e)
        {
            ValidateTime(Time1Entry);
        }

        private void OnTimeCompleted2(object sender, EventArgs e)
        {
            ValidateTime(Time2Entry);
        }

        private void OnTimeCompleted3(object sender, EventArgs e)
        {
            ValidateTime(Time3Entry);
        }

        private void OnTimeCompleted4(object sender, EventArgs e)
        {
            ValidateTime(Time4Entry);
        }

        private void ValidateTime(Entry entry)
        {
            if (!int.TryParse(entry.Text, out int time) || time < 1)
            {
                DisplayAlert("Błąd", "Czas musi być większy niż 1 sekundy i liczbą całkowitą.", "OK");            }
            else 
            { 
                Time1 = int.Parse(Time1Entry.Text);
                Time2 = int.Parse(Time2Entry.Text);
                Time3 = int.Parse(Time3Entry.Text);
                Time4 = int.Parse(Time4Entry.Text);
            }
        }
    }
}