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
    public partial class BeforeGamePage : ContentPage
    {
        public static int timeChanger = 30;
        public BeforeGamePage()
        {
            MessagingCenter.Send(new OrientationMessage { IsLandscape = false }, "SetOrientation");

            InitializeComponent();
            switch (MainPage.gameMode)
            {
                case "allSongs":
                    Category.Text = "Wszystkie gatunki";
                    PhotoCategory.Source = "music.jpg";
                        break;
                case "Disney":
                    Category.Text = "Piosenki Disneya";
                    PhotoCategory.Source = "disney.jpg";
                    break;
                case "Pop":
                    Category.Text = "Pop";
                    PhotoCategory.Source = "pop.jpg";
                    break;
                case "Rock":
                    Category.Text = "Rock";
                    PhotoCategory.Source = "rock.jpg";
                    break;
                case "UsersMusic":
                    Category.Text = "Twoja muzyka";
                    PhotoCategory.Source = "yourMusic.jpg";
                    break;
                case "Rap":
                    Category.Text = "Rap";
                    PhotoCategory.Source = "rap.jpg";
                    break;

            }
        }
        async void Time15_Clicked(object sender, EventArgs e)
        {
            timeChanger = 15;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time30_Clicked(object sender, EventArgs e)
        {
            timeChanger = 30;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time45_Clicked(object sender, EventArgs e)
        {
            timeChanger = 45;
            await Navigation.PushAsync(new RulesPage());
        }

        async void Time60_Clicked(object sender, EventArgs e)
        {
            timeChanger = 60;
            await Navigation.PushAsync(new RulesPage());
        }

        private async void AddNewSongs_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddingNewSongs());
        }

        private async void BackButtonn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}