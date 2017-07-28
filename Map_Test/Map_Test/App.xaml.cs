using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Plugin.Geolocator;

namespace Map_Test
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Map_Test.MapTest();
        }

        protected override void OnStart()
        {
            
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
