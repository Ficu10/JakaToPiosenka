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
        public BeforeGamePage()
        {
            InitializeComponent();
           
        }
        async void Time15_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game());
            MessagingCenter.Send(new OrientationMessage { IsLandscape = true }, "SetOrientation");
        }

        async void Time30_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game());
            MessagingCenter.Send(new OrientationMessage { IsLandscape = true }, "SetOrientation");
        }

        async void Time45_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game());
            MessagingCenter.Send(new OrientationMessage { IsLandscape = true }, "SetOrientation");
        }

        async void Time60_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game());
            MessagingCenter.Send(new OrientationMessage { IsLandscape = true }, "SetOrientation");
        }
    }
}