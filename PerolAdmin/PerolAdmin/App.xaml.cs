using PerolAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class App : Application
    {
        public static AzureDataServices AzureService;
        public App()
        {
           AzureService = new AzureDataServices();
            //InitializeComponent();

            MainPage = new Principal();
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
