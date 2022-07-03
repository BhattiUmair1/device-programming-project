using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms;

namespace Project.Models
{
    public class FlightDetails
    {
        [JsonProperty(PropertyName = "flights")]
        public List<Flight> Flights { get; set; }
        [JsonProperty(PropertyName = "tickets_price")]
        public double TicketsPrice { get; set; }
    }
    public class Flight
    {
        [JsonProperty(PropertyName = "dst")]
        public string FlightFrom { get; set; }
        [JsonProperty(PropertyName = "src")]
        public string FlightTo { get; set; }
        [JsonProperty(PropertyName = "operating_airline")]
        public OperatingAirline Airline { get; set; }
        [JsonProperty(PropertyName = "local_arrival")]
        public DateTime LocalArrival { get; set; }
        [JsonProperty(PropertyName = "local_departure")]
        public DateTime LocalDeparture { get; set; }
        public ImageSource ImageLike { get; set; }
        public ImageSource DImageFlightDepature { get; set; }

        [OnDeserialized]
        private void GetImages(StreamingContext context)
        {
            ImageLike = ImageSource.FromResource("Project.Assets.Like.png");
            DImageFlightDepature = ImageSource.FromResource("Project.Assets.Flight.png");
        }
    }
    public class OperatingAirline
    {
        [JsonProperty(PropertyName = "name")]
        public string NameAirline { get; set; }
    }




}
