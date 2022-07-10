using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ShowFavoriteFlights("");
        }

        public ProfilePage(string Email)
        {
            InitializeComponent();
            Loadicons();
            ShowFavoriteFlights(Email);
        }

        private async void ShowFavoriteFlights(string Email)
        {
            //listView.ItemsSource = await Repository.RepositoryCosmosDB.GetFavoriteFlights(Email);
            var flightsObject = DependencyService.Get<IFlightsRepository>();
            DepartureData[] favoriteFlights = await flightsObject.GetFlightAsync();
            foreach (var flight in favoriteFlights)
            {
                flight.ImageLike = ImageSource.FromResource("Project.Assets.Like.png");
                flight.DImageFlightDepature = ImageSource.FromResource("Project.Assets.Flight.png");
                flight.Trashcan = ImageSource.FromResource("Project.Assets.Trashcan.png");
            }
            listView.ItemsSource = favoriteFlights;
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}