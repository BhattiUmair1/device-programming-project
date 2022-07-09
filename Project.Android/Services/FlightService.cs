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
        private Flights _Flight { get; set; }
        private List<Flights> _Flights { get; set; }
        private JavaDictionary _NewUserInfo { get; set; }
        private JavaDictionary _NewFlight { get; set; }
        private JavaList<JavaDictionary> _NewListOfFlights { get; set; }

        public FlightService()
        {
            _Flights = new List<Flights>();
            _Flight = new Flights();
            _NewFlight = new JavaDictionary();
        }
        public async Task<List<Flights>> GetFlightAsync()
        {
            // TODO get the current id van de user

            FirebaseFirestore db = FirestoreService.Instance;
            QuerySnapshot query = (QuerySnapshot)await db.Collection("Flights").WhereEqualTo(FieldPath.Of("UserId"), "QM5zwFfdnbRgglJ0yKA6r0gDdqo1").Get();
            foreach (var doc in query.Documents)
            {
                IDictionary dictFavFlights = (IDictionary)doc.Data;
                _Flight.AirlineName = dictFavFlights["AirlineName"].ToString();
                _Flight.AirlineToken = dictFavFlights["AirlineToken"].ToString();
                _Flight.UserId = dictFavFlights["UserId"].ToString();
                _Flight.DocumentId = dictFavFlights["DocumentId"].ToString();
                _Flights.Add(_Flight);
            }

            return _Flights;
        }
        public async System.Threading.Tasks.Task AddFlightAsync()
        {
            // TODO: alle data nettjes ophalen en populaten

            FirebaseFirestore db = FirestoreService.Instance;
            string guid = Guid.NewGuid().ToString();
            DocumentReference docRef = db.Collection("Flights").Document(guid);
            // TODO: test of je altijd een nieuwe opject hebt om nieuwe informatie toe te voegen
            
            _NewFlight.Add("AirlineName", "TestAirline");
            _NewFlight.Add("AirlineToken", "FakeTokenId");
            _NewFlight.Add("DocumentId", guid);
            _NewFlight.Add("UserId", "QM5zwFfdnbRgglJ0yKA6r0gDdqo1");

            var testObj = _NewFlight;

            await docRef.Set(_NewFlight);
            _NewFlight.Clear();

            return;
        }
        public async System.Threading.Tasks.Task DeleteFlightAsync()
        {
            // TODO: alle data nettjes ophalen en populaten
            FirebaseFirestore db = FirebaseFirestore.Instance;
            //FirebaseFirestore db = FirestoreService.Instance;
            var docRef = db.Collection("Flights").Document("xYKiFeXCXyvaH1KWRsKj");
            await docRef.Delete();


            return;
        }
    }
}