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
using Xamarin.Forms.Shapes;
using Project.Interfaces;
using Firebase.Storage;

namespace Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public int FlightType { get; set; }
        public string EmailAdress { get; set; }
        public bool BtnEnabled { get; set; }
        public static string ProfilePicture { get; set; } = null;
        private string[] _Capitals { get; set; } = new string[] {
  "Abu Dhabi",
  "Abuja",
  "Accra",
  "Addis Ababa",
  "Algiers",
  "Amman",
  "Amsterdam",
  "Andorra la Vella",
  "Ankara",
  "Antananarivo",
  "Apia",
  "Ashgabat",
  "Asmara",
  "Asuncion",
  "Athens",
  "Baghdad",
  "Baku",
  "Bamako",
  "Bandar Seri Begawan",
  "Bangkok",
  "Bangui",
  "Banjul",
  "Basseterre",
  "Beijing",
  "Beirut",
  "Belgrade",
  "Belmopan",
  "Berlin",
  "Bern",
  "Bishkek",
  "Bissau",
  "Bloemfontein",
  "Bogota",
  "Brasilia",
  "Bratislava",
  "Brazzaville",
  "Bridgetown",
  "Brussels",
  "Bucharest",
  "Budapest",
  "Buenos Aires",
  "Cairo",
  "Canberra",
  "Cape Town",
  "Caracas",
  "Castries",
  "Chisinau",
  "Conakry",
  "Copenhagen",
  "Dakar",
  "Damascus",
  "Dhaka",
  "Dili",
  "Djibouti (city)",
  "Dodoma",
  "Doha",
  "Dublin",
  "Dushanbe",
  "Freetown",
  "Funafuti",
  "Gaborone",
  "Georgetown",
  "Gitega",
  "Guatemala City",
  "Hanoi",
  "Harare",
  "Havana",
  "Helsinki",
  "Honiara",
  "Islamabad",
  "Jakarta",
  "Jerusalem",
  "Jerusalem (East)",
  "Juba",
  "Kabul",
  "Kampala",
  "Kathmandu",
  "Khartoum",
  "Kigali",
  "Kingston",
  "Kingstown",
  "Kinshasa",
  "Kuala Lumpur",
  "Kuwait City",
  "Kyiv",
  "La Paz",
  "Libreville",
  "Lilongwe",
  "Lima",
  "Lisbon",
  "Ljubljana",
  "Lobamba",
  "Lome",
  "London",
  "Luanda",
  "Lusaka",
  "Luxembourg (city)",
  "Madrid",
  "Majuro",
  "Malabo",
  "Male",
  "Managua",
  "Manama",
  "Manila",
  "Maputo",
  "Maseru",
  "Mbabane",
  "Mexico City",
  "Minsk",
  "Mogadishu",
  "Monaco",
  "Monrovia",
  "Montevideo",
  "Moroni",
  "Moscow",
  "Muscat",
  "Nairobi",
  "Nassau",
  "Naypyidaw",
  "N'Djamena",
  "New Delhi",
  "Ngerulmud",
  "Niamey",
  "Nicosia",
  "Nouakchott",
  "Nuku?alofa",
  "Nur-Sultan",
  "Oslo",
  "Ottawa",
  "Ouagadougou",
  "Oyala",
  "Palikir",
  "Panama City",
  "Paramaribo",
  "Paris",
  "Phnom Penh",
  "Podgorica",
  "Port Louis",
  "Port Moresby",
  "Port of Spain",
  "Port Vila",
  "Port-au-Prince",
  "Porto-Novo",
  "Prague",
  "Praia",
  "Pretoria",
  "Pristina",
  "Pyongyang",
  "Quito",
  "Rabat",
  "Reykjavik",
  "Riga",
  "Riyadh",
  "Rome",
  "Roseau",
  "Saint George's",
  "Saint John's",
  "San Jose",
  "San Marino",
  "San Salvador",
  "Sana'a",
  "Santiago",
  "Santo Domingo",
  "Sao Tome",
  "Sarajevo",
  "Seoul",
  "Singapore",
  "Skopje",
  "Sofia",
  "Sri Jayawardenepura Kotte",
  "Stockholm",
  "Sucre",
  "Suva",
  "Taipei",
  "Tallinn",
  "Tarawa",
  "Tashkent",
  "Tbilisi",
  "Tegucigalpa",
  "Tehran",
  "Thimphu",
  "Tirana",
  "Tokyo",
  "Tripoli",
  "Tunis",
  "Ulaanbaatar",
  "Vaduz",
  "Valletta",
  "Vatican City",
  "Victoria",
  "Vienna",
  "Vientiane",
  "Vilnius",
  "Warsaw",
  "Washington D.C.",
  "Wellington",
  "Windhoek",
  "Yamoussoukro",
  "Yaounde",
  "Yaren District",
  "Yerevan",
  "Zagre"
        };
        public bool IsFromCitySelected { get; set; }
        public bool IsToCitySelected { get; set; }
        public FirebaseLoginAndSignupRespons _UserInfo { get; set; }


        public SearchPage()
        {
            var instanceOfUserInfo = DependencyService.Get<IFirebaseAuthentication>();
            _UserInfo = instanceOfUserInfo.GetCurrentUser();
            InitializeComponent();
            LoadIcons();
            UserProfile();
            AddCapitalsToPickers();
        }
        private void AddCapitalsToPickers()
        {
            foreach (var capital in _Capitals)
            {
                pickerFrom.Items.Add(capital);
                pickerTo.Items.Add(capital);
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (SearchPage.ProfilePicture != null)
            {
                imgProfile.Source = SearchPage.ProfilePicture;
            }
        }
        private async void UserProfile()
        {
            var photoUrl = "";
            try
            {
                var task = new FirebaseStorage("flightsappxamarin.appspot.com", new FirebaseStorageOptions
                {
                    ThrowOnCancel = true,
                }).Child(_UserInfo.Uid).Child("profilePicture.jpeg").GetDownloadUrlAsync();
                photoUrl = await task;
            }
            catch (Exception)
            {
                photoUrl = null;
            }

            if (photoUrl != null)
            {
                imgProfile.Source = photoUrl;
                SearchPage.ProfilePicture = photoUrl;
            } else
            {
                imgProfile.Source = ImageSource.FromResource("Project.Assets.DefaultPicture.png");
            }
        }
        private void LoadIcons()
        {
            imgbackground.Source = ImageSource.FromResource("Project.Assets.Background.png");
            imgdeparture.Source = ImageSource.FromResource("Project.Assets.Departure_icon.png");
        }

        private async void TapGestureRecognizer_Profile(object sender, EventArgs e)
        {
            imgProfile.Opacity = 0.7;
            await Task.Delay(200);
            imgProfile.Opacity = 1;
            await Navigation.PushAsync(new ProfilePage());
        }

        private void pickerFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker item = sender as Picker;
            if (item.SelectedItem != null)
            {
                IsFromCitySelected = true;
            }
            else
            {
                IsFromCitySelected = false;
            }

            if (IsFromCitySelected == true && IsToCitySelected == true)
            {
                searchbtnframe.Opacity = 1;
                searchbtnframe.IsEnabled = true;
            }
            else
            {
                searchbtnframe.Opacity = 0.5;
                searchbtnframe.IsEnabled = false;
            }
        }

        private void pickerTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker item = sender as Picker;
            if (item.SelectedItem != null)
            {
                IsToCitySelected = true;
            }
            else
            {
                IsToCitySelected = false;
            }

            if (IsFromCitySelected == true && IsToCitySelected == true)
            {
                searchbtnframe.Opacity = 1;
                searchbtnframe.IsEnabled = true;
            }
            else
            {
                searchbtnframe.Opacity = 0.5;
                searchbtnframe.IsEnabled = false;
            }
        }

        private async void TapGestureRecognizer_Search(object sender, EventArgs e)
        {
            frameErrorBox.IsVisible = false;
            searchbtnframe.Opacity = 0.7;
            await Task.Delay(200);
            searchbtnframe.Opacity = 1;
            string CityFrom = pickerFrom.SelectedItem.ToString().Substring(0, 3).ToUpper();
            string CityTo = pickerTo.SelectedItem.ToString().Substring(0, 3).ToUpper();
            string CityFromDate = dateFrom.Date.ToString("dd/MM/yyyy");
            var currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            var date1 = DateTime.ParseExact(CityFromDate, "dd/MM/yyyy", null);
            var date2 = DateTime.ParseExact(currentDate, "dd/MM/yyyy", null);
            var result = DateTime.Compare(date1, date2);
            if (result < 0)
            {
                lblErrorMessage.Text = "Departure date cannot be older than today's date.";
                frameErrorBox.IsVisible = true;
                return;
            }

            Navigation.PushAsync(new ResultPage(CityTo, CityFromDate, CityFrom, EmailAdress));
            searchbtnframe.BackgroundColor = Color.FromHex("#3A3A3A");
        }
    }
}