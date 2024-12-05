using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace JakaToPiosenka
{
    public partial class App : Application
    {
        public static SQLiteHelper db;


        public App()
        {
            InitializeComponent();
            // Ustaw SplashPage jako stronę startową
            MainPage = new NavigationPage(new SplashPage());

        }

       
        protected override void OnStart()
        {
            // Handle when your app starts
          
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
