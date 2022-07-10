using Project.Views;
using System;
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

            MainPage = new NavigationPage(new ProfilePage());
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
