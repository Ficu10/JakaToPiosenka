using JakaToPiosenka;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JakaToPiosenka
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
            AnimateSplash();
        }

        private async void AnimateSplash()
        {
            // Logo: Pojawienie i powiększenie
            await Logo.FadeTo(1, 2000); // Stopniowe pojawienie
            await ZgadujZgadula.TranslateTo(0, 400, 2000, Easing.BounceOut); // Przesunięcie w dół
            await Logo.ScaleTo(1.2, 1000); // Powiększenie
            await Logo.ScaleTo(1, 500); // Powrót do normalnej wielkości

            // Tekst powitalny: Stopniowe pojawienie
            await WelcomeText.FadeTo(1, 2000);

            // Przejście do głównej strony
            await Task.Delay(1000); // Pauza na wyświetlenie animacji
            Application.Current.MainPage = new NavigationPage(new MainPage()); // Główna strona aplikacji
        }
    }
}
