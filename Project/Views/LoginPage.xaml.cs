using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Loadicons();
        }

        private void Loadicons()
        {
            imgbackground.Source = ImageSource.FromResource("Project.Assets.Background.png");
        }

        private async Task<FirebaseLoginRespons> LoginWithEmailAndPasswordAsync(string email, string password)
        {
            var firebase = DependencyService.Get<IFirebaseAuthentication>();
            //var result = await firebase.LoginWithEmailAndPassword(email, password);

            // trigger a loading indicator
            FirebaseLoginRespons respons = await firebase.LoginWithEmailAndPassword("xamarin@testuser.com", "test123456");

            // check if you have a response => remove the loading indicator

            return respons;

        }


        private async Task<DepartureData[]> GetFLightAsync()
        {
            var flightsObject = DependencyService.Get<IFlightsRepository>();
            return await flightsObject.GetFlightAsync();
        }

        private async Task AddFLightAsync()
        {
            var flightsObject = DependencyService.Get<IFlightsRepository>();
            DepartureData EmptyObject = new DepartureData();
            await flightsObject.AddFlightAsync(EmptyObject);
            return;
        }
        private async Task DeleteFlightAsync()
        {
            var flightsObject = DependencyService.Get<IFlightsRepository>();
            await flightsObject.DeleteFlightAsync();
            return;
        }

        private async void TapGestureRecognizer_TappedAsync(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entEmail.Text) && !string.IsNullOrWhiteSpace(entPassword.Text))
            {
                FirebaseLoginRespons respons = await LoginWithEmailAndPasswordAsync(entEmail.Text, entPassword.Text);

                //await AddFLightAsync();
                //await DeleteFlightAsync();
                if (respons.IsError != true)
                {
                    await Navigation.PushAsync(new SearchPage(entEmail.Text, entPassword.Text));
                }
                else
                {
                    Console.WriteLine(respons.ErrorMessage);
                }
            }
        }
    }
}