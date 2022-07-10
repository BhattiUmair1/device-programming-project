using Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        public string EmailAdress { get; set; }
        public ResultPage()
        {
            InitializeComponent();
            LoadIcons();
        }
        public ResultPage(string CityTo, string CityFromDate, string CityFrom, string Email)
        {
            InitializeComponent();
            LoadIcons();
            EmailAdress = Email;
            ShowResults(CityTo, CityFromDate, CityFrom);
        }

        private async void ShowResults(string CityTo, string CityFromDate, string CityFrom)
        {
            var Flights = await Repository.Repository.GetDepatureFlightsAsync(CityTo, CityFrom, CityFromDate);
            foreach (var item in Flights.DData)
            {
                item.Email = EmailAdress;
            }
            
            listView.ItemsSource = Flights.DData;
        }

        private void LoadIcons()
        {
            imgbackground.Source = ImageSource.FromResource("Project.Assets.Background.png");
            imgbackarrow.Source = ImageSource.FromResource("Project.Assets.Back_arrow.png");
        }
        private void TapGestureRecognizer_BackToSearch(object sender, EventArgs e)
        {
            Debug.WriteLine("Going back..");
            Navigation.PopAsync();
        }
        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = listView.SelectedItem as DepartureData;
            Navigation.PushAsync(new FlightDetailPage(item.DBookingToken));
        }
        private void TapGestureRecognizer_Like(object sender, EventArgs e)
        {
            Debug.WriteLine("tap works");
        }
    }
}