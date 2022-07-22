using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            Loadicons();
            ShowFavoriteFlightsAsync();
        }

        private async void ShowFavoriteFlightsAsync()
        {
            //listView.ItemsSource = await Repository.RepositoryCosmosDB.GetFavoriteFlights(Email);
            var flightsObject = DependencyService.Get<IFlightsRepository>();
            var favoriteFlights = await flightsObject.GetFlightAsync();
            if (favoriteFlights != null)
            {
                foreach (var flight in favoriteFlights)
                {
                    flight.ImageLike = ImageSource.FromResource("Project.Assets.Like.png");
                    flight.DImageFlightDepature = ImageSource.FromResource("Project.Assets.Flight.png");
                    flight.Trashcan = ImageSource.FromResource("Project.Assets.Trashcan.png");
                }
                listView.ItemsSource = favoriteFlights;
            }
            else
            {
                listView.ItemsSource = null;
            }
        }

        private void Loadicons()
        {
            imgbackground.Source = ImageSource.FromResource("Project.Assets.Background.png");
            imgbackarrow.Source = ImageSource.FromResource("Project.Assets.Back_arrow.png");
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = listView.SelectedItem as DepartureData;
            Navigation.PushAsync(new FlightDetailPage(item.DBookingToken));
        }

        private async void TapGestureRecognizer_BackToSearch(object sender, EventArgs e)
        {
            backframe.BackgroundColor = Color.FromHex("#e5e5e5");
            backframe.Opacity = 0.9;
            Console.WriteLine("Going back..");
            await Navigation.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Image trashIcon = sender as Image;
            trashIcon.Opacity = 0.7;
            await Task.Delay(100);
            trashIcon.Opacity = 1;

            ShowFavoriteFlightsAsync();
        }
    }
}