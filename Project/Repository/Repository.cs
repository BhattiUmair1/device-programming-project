using Project.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace Project.Repository
{
    class Repository
    {

        public static object JsonConvert { get; private set; }
        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json");
            client.DefaultRequestHeaders.Add("apikey", "bTD4EaA_RUVOxm2wAx4JITRizn4wYMBA");
            return client;
        }
        public static async Task<DepartureFlight> GetDepatureFlightsAsync(string CityFrom, string CityTo, string Date)
        {
            using (HttpClient client = GetHttpClient())
            {
                string url = $"https://tequila-api.kiwi.com/v2/search?fly_from={CityFrom}&fly_to={CityTo}&dateFrom={Date}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DepartureFlight>(json);
                        return result;
                    }
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public static async Task<FlightDetails> GetSelectedFlightInfoAsync(string BookingToken)
        {
            using (HttpClient client = GetHttpClient())
            {
                string url = $"https://tequila-api.kiwi.com/v2/booking/check_flights?bnum=1&pnum=1&currency=EUR&booking_token={BookingToken}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<FlightDetails>(json);
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
