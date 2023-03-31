using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace JakaToPiosenka.Droid
{
    [Activity(Label = "JakaToPiosenka", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation , ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
          
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            MessagingCenter.Subscribe<OrientationMessage>(this, "SetOrientation", (message) =>
            {
                if (message.IsLandscape)
                {
                    RequestedOrientation = ScreenOrientation.Landscape;
                }
                else
                {
                    RequestedOrientation = ScreenOrientation.Portrait;
                }
            });
            SupportActionBar.Hide();
            base.OnCreate(savedInstanceState);


            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
      

    }
    
}