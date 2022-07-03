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
    public partial class FlightDetailPage : ContentPage
    {
        public FlightDetailPage()
        {
            InitializeComponent();
            Loadicons();
        }

        public FlightDetailPage(string BookingToken)
        {
            InitializeComponent();
            Loadicons();
            ShowFlightDetails(BookingToken);
        }

        private async void ShowFlightDetails(string BookingToken)
        {
            FlightDetails Flights = await Repository.Repository.GetSelectedFlightInfoAsync(BookingToken);
            lvwFlightDetails.ItemsSource = Flights.Flights;
            //foreach (var airline in Flights.Flights)
            //{
            //    lvwFlightDetails.BindingContext = airline;
            //}
        }

        private void Loadicons()
        {
            imgbackground.Source = ImageSource.FromResource("Project.Assets.Background.png");
            imgbackarrow.Source = ImageSource.FromResource("Project.Assets.Back_arrow.png");
            //imgflight.Source = ImageSource.FromResource("Project.Assets.Flight.png");
            //imgreturnflight.Source = ImageSource.FromResource("Project.Assets.Returnflight.png");
        }

        private async void TapGestureRecognizer_BackToSearch(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}