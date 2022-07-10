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

[assembly: Xamarin.Forms.Dependency(typeof(FlightService))]
namespace Project.Droid.Firebase.Repository
{
    public class FlightService : IFlightsRepository

    {

        // get data props
        private DepartureData[] _Trips { get; set; } = null;
        private DepartureData _Trip { get; set; }
        private DepartureRoute[] _FlightRoutes { get; set; } = null;
        private DepartureRoute _Route { get; set; }

        // post data props
        private JavaList<JavaDictionary> _NewFlightRoutes { get; set; }
        private JavaDictionary _NewTrip { get; set; }

        // current logged in user
        private FirebaseUser _User { get; set; }
        public FlightService()
        {
            _Route = new DepartureRoute();
            _NewFlightRoutes = new JavaList<JavaDictionary>();
            _NewTrip = new JavaDictionary();
            _User = FirebaseAuth.GetInstance(FirestoreService.app).CurrentUser;
        }
        public async Task<DepartureData[]> GetFlightAsync()
        {
            // TODO get the current id van de user
            
            FirebaseFirestore db = FirestoreService.Instance;
            //QuerySnapshot query = (QuerySnapshot)await db.Collection("Flights").WhereEqualTo(FieldPath.Of("userId"), _User.Uid).Get();
            QuerySnapshot query = (QuerySnapshot)await db.Collection("Flights").WhereEqualTo(FieldPath.Of("userId"), "QM5zwFfdnbRgglJ0yKA6r0gDdqo1").Get();

            DepartureData[] trips = new DepartureData[1];

            int routeIndex;
            int docIndex = 0;
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

                var routes = (ICollection)tripObj["routes"];

                foreach (var routeDoc in routes)
                {
                    if (_FlightRoutes == null || routeIndex == 0)
                    {
                        _FlightRoutes = null;
                        _FlightRoutes = new DepartureRoute[routes.Count];
                    }
                    var routeObj = (IDictionary)routeDoc;
                    _Route.DId = routeObj["id"].ToString();
                    _Route.DCityFrom = routeObj["cityFrom"].ToString();
                    _Route.DCityCodeFrom = routeObj["cityCodeFrom"].ToString();
                    _Route.DCityTo = routeObj["cityTo"].ToString();
                    _Route.DCityCodeTo = routeObj["cityCodeTo"].ToString();
                    _Route.DAirline = routeObj["airline"].ToString();
                    _Route.DLocalArrival = DateTime.Parse(routeObj["local_arrival"].ToString());
                    _Route.DLocalDeparture = DateTime.Parse(routeObj["local_departure"].ToString());

                    _FlightRoutes[routeIndex] = _Route;
                    routeIndex = routeIndex + 1;
                }
                
                _Trip.DRoute = _FlightRoutes;
                _Trips.SetValue(_Trip, docIndex);
                docIndex = docIndex + 1;
                var stop = _Trips;
            }

            

            return _Trips;
        }
        public async System.Threading.Tasks.Task AddFlightAsync(DepartureData trip)
        {
            // TODO: alle data nettjes ophalen en populaten

            _NewTrip.Add("userId", _User.Uid);
            _NewTrip.Add("id", trip.Id);
            _NewTrip.Add("cityCodeFrom", trip.DCityFromCode);
            _NewTrip.Add("cityCodeTo", trip.DCityToCode);
            _NewTrip.Add("price", trip.DPrice);
            _NewTrip.Add("booking_token", trip.DBookingToken);
            _NewTrip.Add("local_arrival", trip.DReturn.ToString());
            _NewTrip.Add("local_departure", trip.DDeparture.ToString());

            var flights = trip.DRoute;
            foreach (var flight in flights)
            {
                JavaDictionary _NewRoute = new JavaDictionary();
                _NewRoute.Add("id", flight.DId);
                _NewRoute.Add("cityFrom", flight.DCityFrom);
                _NewRoute.Add("cityCodeFrom", flight.DCityCodeFrom);
                _NewRoute.Add("cityTo", flight.DCityTo);
                _NewRoute.Add("cityCodeTo", flight.DCityCodeTo);
                _NewRoute.Add("airline", flight.DAirline);
                _NewRoute.Add("local_arrival", flight.DLocalArrival.ToString());
                _NewRoute.Add("local_departure", flight.DLocalDeparture.ToString());
                _NewFlightRoutes.Add(_NewRoute);
            }
            _NewTrip.Add("routes", _NewFlightRoutes);

            FirebaseFirestore db = FirestoreService.Instance;
            DocumentReference docRef = db.Collection("Flights").Document(trip.Id);
            //TODO: test of je altijd een nieuwe opject hebt om nieuwe informatie toe te voegen

            await docRef.Set(_NewTrip);
            _NewTrip.Clear();

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