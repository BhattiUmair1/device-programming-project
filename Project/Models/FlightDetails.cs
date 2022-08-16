using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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

        public string FlightTime { get; set; }

        public ImageSource ImageLike { get; set; }
        public ImageSource DImageFlightDepature { get; set; }

        [OnDeserialized]
        private void CallMethods(StreamingContext context)
        {
            AddImages();
            CalDuration();
        }
        private void AddImages()
        {
            ImageLike = ImageSource.FromResource("Project.Assets.Like.png");
            DImageFlightDepature = ImageSource.FromResource("Project.Assets.Flight.png");
        }
        private void CalDuration()
        {
            TimeSpan timeSpan = LocalArrival.Subtract(LocalDeparture);
            var timeSplitUp = timeSpan.ToString().Split(':');
            FlightTime = timeSplitUp[0] + "h " + timeSplitUp[1] + "m";
        }
    }
    public class OperatingAirline
    {
        [JsonProperty(PropertyName = "name")]
        public string NameAirline { get; set; }
    }

}
