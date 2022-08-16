using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Interfaces;
using Project.Repository;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Project.Models
{

    public class DepartureFlight
    {
        [JsonProperty(PropertyName = "data")]
        public DepartureData[] DData { get; set; }
        //public ReturnData[] RData { get; set; }
    }
    public class DepartureData
    {
        public DepartureData CurrentObject { get; private set; }
        public DepartureData()
        {
            LikeImageTapCommand = new Command(LikeImageTapMethod);
            TashImageTapCommand = new Command<object>(async (obj) => await TashImageTapMethodAsync(obj));

        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "cityCodeFrom")]
        public string DCityFromCode { get; set; }

        [JsonProperty(PropertyName = "cityCodeTo")]
        public string DCityToCode { get; set; }

        [JsonProperty(PropertyName = "price")]
        public int DPrice { get; set; }

        [JsonProperty(PropertyName = "route")]
        public DepartureRoute[] DRoute { get; set; }

        [JsonProperty(PropertyName = "booking_token")]
        public string DBookingToken { get; set; }

        [JsonProperty(PropertyName = "local_arrival")]
        public DateTime DReturn { get; set; }

        [JsonProperty(PropertyName = "local_departure")]
        public DateTime DDeparture { get; set; }

        public string TripTime { get; set; }
        public string DepartureTripTime { get; set; }
        public string ArrivalTripTime { get; set; }

        public ImageSource ImageLike { get; set; }

        public ImageSource DImageFlightDepature { get; set; }

        public ImageSource Trashcan { get; set; }

        public string Email { get; set; }

        [OnDeserialized]
        public void CallMethods(StreamingContext context)
        {
            CalDuration();
            AddImages();
            CalFlightTimes();
        }
        private void CalFlightTimes()
        {
            DepartureTripTime = CheckIfTwoCharacters(DDeparture.Hour.ToString()) + ":" + CheckIfTwoCharacters(DDeparture.Minute.ToString());
            ArrivalTripTime = CheckIfTwoCharacters(DReturn.Hour.ToString()) + ":" + CheckIfTwoCharacters(DReturn.Minute.ToString());
        }

        private string CheckIfTwoCharacters(string digits)
        {
            if (digits.Length < 2)
            {
                digits = "0" + digits;
            }
            return digits;
        }

        private void CalDuration()
        {
            TimeSpan timeSpan = DReturn.Subtract(DDeparture);
            var timeSplitUp = timeSpan.ToString().Split(':');
            TripTime = timeSplitUp[0] + "h " + timeSplitUp[1] + "m";
        }

        private void AddImages()
        {
            ImageLike = ImageSource.FromResource("Project.Assets.Like.png");
            DImageFlightDepature = ImageSource.FromResource("Project.Assets.Flight.png");
            Trashcan = ImageSource.FromResource("Project.Assets.Trashcan.png");
        }
        // "interne logical"
        public ICommand LikeImageTapCommand { get; private set; }
        public ICommand TashImageTapCommand { get; private set; }

        void LikeImageTapMethod(object obj)
        {
            Debug.WriteLine("Add");
            var SelectedObject = obj as DepartureData;
            var flightsObject = DependencyService.Get<IFlightsRepository>();
            flightsObject.AddFlightAsync(SelectedObject);
        }

        async Task TashImageTapMethodAsync(object obj)
        {
            Debug.WriteLine("Deleted");
            CurrentObject = obj as DepartureData;
            var flightsObject = DependencyService.Get<IFlightsRepository>();
            await flightsObject.DeleteFlightAsync(CurrentObject.Id);
            //RepositoryCosmosDB.DeleteFlight(CurrentObject);

        }
        public class DepartureRoute
        {
            [JsonProperty(PropertyName = "id")]
            public string DId { get; set; }

            [JsonProperty(PropertyName = "cityFrom")]
            public string DCityFrom { get; set; }

            [JsonProperty(PropertyName = "cityCodeFrom")]
            public string DCityCodeFrom { get; set; }

            [JsonProperty(PropertyName = "cityTo")]
            public string DCityTo { get; set; }

            [JsonProperty(PropertyName = "cityCodeTo")]
            public string DCityCodeTo { get; set; }

            [JsonProperty(PropertyName = "airline")]
            public string DAirline { get; set; }

            [JsonProperty(PropertyName = "local_arrival")]
            public DateTime DLocalArrival { get; set; }

            [JsonProperty(PropertyName = "local_departure")]
            public DateTime DLocalDeparture { get; set; }
        }
    }
}