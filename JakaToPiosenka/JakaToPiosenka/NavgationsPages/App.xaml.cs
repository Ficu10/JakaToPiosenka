using System;
using System.IO;
using JakaToPiosenka.HelpClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace JakaToPiosenka
{
    public partial class App : Application
    {
        public static SQLiteHelper db;

        //public static SQLiteHelper MyDatabase
        //{

        //    get
        //    {
        //        if (db == null)
        //        {
        //            db = new SQLiteHelper(+);
        //        }
        //        return db;
        //    }
        //}

        public App()
        {
            InitializeComponent();
           
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
