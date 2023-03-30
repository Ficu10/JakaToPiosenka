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
        List<string> myList = new List<string>() { "1", "1", "1", "1", "1", "1", "1", "1", "1", };

        public AfterGame()
        {
            InitializeComponent();

            myList.Clear();
            for (int i = 0; i < Game.songsFromGame.Count; i++)
            {
                myList.Add(Game.songsFromGame[i]);
            }
            myListView.ItemsSource = myList;
            MessagingCenter.Send(new OrientationMessage { IsLandscape = false }, "SetOrientation");
        }

        private async void RestartGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RulesPage());
        }

        private void ShowSongs_Clicked(object sender, EventArgs e)
        {
            myListView.IsVisible = true;
            myList.Clear();
            for (int i = 0; i < Game.songsFromGame.Count; i++)
            {
                myList.Add(Game.songsFromGame[i]);
            }
            myListView.ItemsSource = myList;
        }

        private async void Menu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}