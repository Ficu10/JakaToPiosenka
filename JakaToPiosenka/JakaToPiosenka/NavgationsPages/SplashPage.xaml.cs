﻿using JakaToPiosenka;
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
            // Logo: Pojawienie i powiększenie
            await Logo.FadeTo(1, 1000); // Stopniowe pojawienie
            await Task.WhenAll(
             ZgadujZgadula.TranslateTo(0, 400, 2000, Easing.CubicOut), // Przesunięcie w dół
             Logo.ScaleTo(1.20, 2000) // Powiększenie
             );

            // Tekst powitalny: Stopniowe pojawienie
            await WelcomeText.FadeTo(1, 1000);

            // Przejście do głównej strony
            await Task.Delay(1000); // Pauza na wyświetlenie animacji
            Application.Current.MainPage = new NavigationPage(new MainPage()); // Główna strona aplikacji
        }
    }
}