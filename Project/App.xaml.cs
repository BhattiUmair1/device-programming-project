using Project.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Roboto-Regular.ttf", Alias = "Roboto-R")]
[assembly: ExportFont("Roboto-Medium.ttf", Alias = "Roboto-M")]
[assembly: ExportFont("Roboto-Bold.ttf", Alias = "Roboto-B")]
namespace Project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SearchPage());
        }

        protected override void OnStart()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        protected override void OnSleep()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        protected override void OnResume()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Page page;
            if (MainPage is NavigationPage)
            {
                page = ((NavigationPage)MainPage).CurrentPage;
            }
            else
            {
                page = MainPage;
            }
            if (e.NetworkAccess.ToString() == "Local")
            {
                page.DisplayAlert("Connection lost", "Please make sure you are connected to the internet.", "OK");
            }
        }
    }
}
