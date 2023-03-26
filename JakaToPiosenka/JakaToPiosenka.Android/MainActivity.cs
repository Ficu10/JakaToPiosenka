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
            var decorView = Window.DecorView;
            var uiOptions = (int)decorView.SystemUiVisibility;
            uiOptions |= (int)SystemUiFlags.HideNavigation;
            decorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
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

            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);

            base.OnCreate(savedInstanceState);
            
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

    }
    
}