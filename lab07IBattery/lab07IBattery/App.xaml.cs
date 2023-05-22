using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lab07IBattery
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new BatteryDemo();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
