using Android.Gms.Extensions;
using Android.Runtime;
using Firebase.Auth;
using Firebase.Firestore;
using FirestoreRe.Droid.Services;
using Project.Droid.Firebase.Repository;
using Project.Interfaces;
using Project.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Project.Models.DepartureData;

[assembly: Xamarin.Forms.Dependency(typeof(FlightService))]
namespace Project.Droid.Firebase.Repository
{
    public class FlightService : IFlightsRepository

    {

        // get data props
        private DepartureData[] _Trips { get; set; } = null;
        private DepartureData _Trip { get; set; }

        // post data props
        private JavaList<JavaDictionary> _NewFlightRoutes { get; set; }
        private JavaDictionary _NewTrip { get; set; }

        // current logged in user
        private FirebaseUser _User { get; set; }
        public FlightService()
        {
            _NewFlightRoutes = new JavaList<JavaDictionary>();
            _NewTrip = new JavaDictionary();
            _User = FirebaseAuth.GetInstance(FirestoreService.app).CurrentUser;
        }
        public async Task<DepartureData[]> GetFlightAsync()
        {
            FirebaseFirestore db = FirestoreService.Instance;
            QuerySnapshot query = (QuerySnapshot)await db.Collection("Flights").WhereEqualTo(FieldPath.Of(_User.Uid), "QM5zwFfdnbRgglJ0yKA6r0gDdqo1").Get();

            int routeIndex;
            int docIndex = 0;
            _Trips = null;
            if (query != null)
            {
                foreach (DocumentSnapshot doc in query.Documents)
            {
                routeIndex = 0;
                _Trip = null;
                _Trip = new DepartureData();
                if (_Trips == null)
                {
                    _Trips = new DepartureData[query.Documents.Count];
                }

                IDictionary tripObj = (IDictionary)doc.Data;
                _Trip.Id = tripObj["id"].ToString();
                _Trip.DCityFromCode = tripObj["cityCodeFrom"].ToString();
                _Trip.DCityToCode = tripObj["cityCodeTo"].ToString();
                _Trip.DPrice = Int32.Parse(tripObj["price"].ToString());
                _Trip.DBookingToken = tripObj["booking_token"].ToString();
                _Trip.DReturn = DateTime.Parse(tripObj["local_arrival"].ToString());
                _Trip.DDeparture = DateTime.Parse(tripObj["local_departure"].ToString());
                _Trips.SetValue(_Trip, docIndex);
                docIndex = docIndex + 1;
                var stop = _Trips;
            }
                return _Trips;
            } else
            {
                return null;
            }            
        }
        public async System.Threading.Tasks.Task AddFlightAsync(DepartureData trip)
        {
            _NewTrip.Add("userId", _User.Uid);
            _NewTrip.Add("id", trip.Id);
            _NewTrip.Add("cityCodeFrom", trip.DCityFromCode);
            _NewTrip.Add("cityCodeTo", trip.DCityToCode);
            _NewTrip.Add("price", trip.DPrice);
            _NewTrip.Add("booking_token", trip.DBookingToken);
            _NewTrip.Add("local_arrival", trip.DReturn.ToString());
            _NewTrip.Add("local_departure", trip.DDeparture.ToString());

            FirebaseFirestore db = FirestoreService.Instance;
            DocumentReference docRef = db.Collection("Flights").Document(trip.Id);

            await docRef.Set(_NewTrip);
            _NewTrip.Clear();

            return;
        }
        public async Task DeleteFlightAsync(string id)
        {
            FirebaseFirestore db = FirestoreService.Instance;
            var docRef = db.Collection("Flights").Document(id);
            await docRef.Delete();
            return;
        }
    }
}