using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;
using System.IO;
using Xamarin.Essentials;

namespace JakaToPiosenka
{
    public partial class MainPage : ContentPage
    {
        public static bool orientationPortrait = true;
        async void AllSongsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeforeGamePage());

        }

        async void RockButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeforeGamePage());

        }

        async void PopButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeforeGamePage());

        }

        async void DisneyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeforeGamePage());

        }

        async void UsersMusicButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeforeGamePage());

        }

        async void RapButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeforeGamePage());

        }
    }
}
