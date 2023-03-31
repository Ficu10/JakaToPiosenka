using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JakaToPiosenka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AfterGame : ContentPage
    {
        public AfterGame()
        {
            InitializeComponent();
            Resut.Text = "Twoj wynik to " + Game.pointsCounter.ToString() + "/10";
            myListView.ItemsSource = Game.songsFromGame;
            Game.pointsCounter = 0;
            MessagingCenter.Send(new OrientationMessage { IsLandscape = false }, "SetOrientation");
        }

        private async void RestartGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RulesPage());
        }

       
        private async void Menu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}