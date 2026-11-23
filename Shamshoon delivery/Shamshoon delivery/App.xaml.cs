using Shamshoon_delivery.Services;
using Shamshoon_delivery.Views;
using Shamshoon_delivery.Data; // <--- Thabbet fi hedhi
using System;
using System.IO;              // <--- Thabbet fi hedhi
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shamshoon_delivery
{
    public partial class App : Application
    {
        // 1. Déclaration de l'instance statique de la base (Singleton kima TP 3)
        static OrderDatabase database;
        public static OrderDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new OrderDatabase(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShamshoonDelivery.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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