using Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public int FlightType { get; set; }
        public string EmailAdress { get; set; }
        public SearchPage()
        {
            InitializeComponent();
            FlightType = 1;
            LoadIcons();
        }

        public SearchPage(string Email)
        {
            InitializeComponent();
            FlightType = 1;
            LoadIcons();
            EmailAdress = Email;
        }
        private void LoadIcons()
        {
            imgbackground.Source = ImageSource.FromResource("Project.Assets.Background.png");
            imgdeparture.Source = ImageSource.FromResource("Project.Assets.Departure_icon.png");
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entFrom.Text) && !string.IsNullOrWhiteSpace(entTo.Text) && !string.IsNullOrWhiteSpace(Convert.ToString(dateFrom.Date)))
            {
                if (FlightType == 1)
                {
                    Debug.WriteLine("Searching...");
                    string CityFrom = entFrom.Text.Substring(0, 3).ToUpper();
                    Debug.WriteLine(CityFrom);
                    string CityTo = entTo.Text.Substring(0, 3).ToUpper();
                    string CityFromDate = dateFrom.Date.ToString("dd/MM/yyyy");
                    await Navigation.PushAsync(new ResultPage(CityTo, CityFromDate, CityFrom, EmailAdress));
                }
            };
        }
        private void TapGestureRecognizer_TwoWay(object sender, EventArgs e)
        {
            lblTwoWay.BackgroundColor = Color.FromHex("#737373");
            lblTwoWay.TextColor = Color.White;
            frameTwoWay.BackgroundColor = Color.FromHex("#737373");

            lblOneWay.BackgroundColor = Color.White;
            lblOneWay.TextColor = Color.FromHex("#A1A1A1");
            frameOneWay.BackgroundColor = Color.Transparent;

            FlightType = 2;
        }
        private void TapGestureRecognizer_OneWay(object sender, EventArgs e)
        {
            lblOneWay.BackgroundColor = Color.FromHex("#737373");
            lblOneWay.TextColor = Color.White;
            frameOneWay.BackgroundColor = Color.FromHex("#737373");

            lblTwoWay.BackgroundColor = Color.White;
            lblTwoWay.TextColor = Color.FromHex("#A1A1A1");
            frameTwoWay.BackgroundColor = Color.Transparent;

            FlightType = 1;
        }

        private async void TapGestureRecognizer_Profile(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage(EmailAdress));
        }
    }
}