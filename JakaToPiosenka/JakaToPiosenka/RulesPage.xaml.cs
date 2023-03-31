using System;
using System.Collections.Generic;
using System.Dynamic;
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
            // Start receiving accelerometer data updates
            Accelerometer.Start(SensorSpeed.UI);

            // Subscribe to the ReadingChanged event to receive accelerometer data updates
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        private async void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var acceleration = e.Reading.Acceleration;
            var x = acceleration.X;
            var y = acceleration.Y;
            var z = acceleration.Z;

            //Rules1.Text = "x: " + x.ToString();
            //Rules2.Text = "y: " + y.ToString();
            //Rules3.Text = "z: " + z.ToString();
            if (x >= 1 && y < 0.2 && z < 0.2)
            {
                Dispose();
                await Navigation.PushAsync(new Game());
            }

        }

        public void Dispose()
        {
            Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
        }
       
    }









            // Convert the orientation angles to

            

    
}