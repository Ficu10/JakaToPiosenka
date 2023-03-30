using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JakaToPiosenka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RulesPage : ContentPage
    {
        public RulesPage()
        {
            InitializeComponent();
            MessagingCenter.Send(new OrientationMessage { IsLandscape = true }, "SetOrientation");
        }

     

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game());
        }

    }
}