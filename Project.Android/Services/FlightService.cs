using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using FirestoreRe.Droid.Services;
using Java.Lang;
using Newtonsoft.Json;
using Project.Droid.Firebase.Repository;
using Project.Interfaces;
using Project.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(FlightService))]
namespace Project.Droid.Firebase.Repository
{
    public class FlightService : IFlightsRepository
    {
        private UserInfo _FavoriteFlights { get; set; }
        private Flights _Flight { get; set; }
        private List<Flights> _Flights { get; set; }
        public FlightService()
        {
            _FavoriteFlights = new UserInfo();
            _Flights = new List<Flights>();
            _Flight = new Flights();
        }
        public async Task<UserInfo> GetFlightAsync()
        {
            FirebaseFirestore db = FirestoreService.Instance;
            DocumentReference docRef = db.Collection("Flights").Document("QM5zwFfdnbRgglJ0yKA6r0gDdqo1");
            DocumentSnapshot doc = (DocumentSnapshot)await docRef.Get();
            var FavoriteFlights = (IEnumerable)doc.Data["FavoriteFlights"];
            _FavoriteFlights.UserID = doc.Data["UserId"].ToString();
            foreach (var item in FavoriteFlights)
            {
                IDictionary dictFavFlights = (IDictionary)item;
                _Flight.AirlineName = dictFavFlights["AirlineName"].ToString();
                _Flight.AirlineToken = dictFavFlights["AirlineToken"].ToString();
                _Flights.Add(_Flight);
            }
            _FavoriteFlights.Flights = _Flights;

            return _FavoriteFlights;
        }

    }
}