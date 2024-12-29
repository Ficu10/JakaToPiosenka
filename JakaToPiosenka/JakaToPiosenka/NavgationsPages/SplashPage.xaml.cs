using JakaToPiosenka;
using JakaToPiosenka.HelpClasses;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JakaToPiosenka
{
    public partial class SplashPage : ContentPage
    {
        Sounds sound = new Sounds();

        public SplashPage()
        {
            InitializeComponent();
            AnimateSplash();
        }

        private async void AnimateSplash()
        {
            sound.StartSound();

            // Logo: Fade in and scale
            await Logo.FadeTo(1, 1000);
            await Task.WhenAll(
                ZgadujZgadula.TranslateTo(0, Logo.Y - ZgadujZgadula.Height - 20, 2000, Easing.CubicOut), // Adjust for position above logo
                Logo.ScaleTo(1.2, 2000) // Enlarge logo
            );

            // Welcome Text: Fade in
            await WelcomeText.FadeTo(1, 2000);

            // Transition to main page
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

    }
}
