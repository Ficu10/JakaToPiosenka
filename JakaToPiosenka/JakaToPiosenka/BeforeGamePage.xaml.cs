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
            InitializeComponent();
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
    }
}