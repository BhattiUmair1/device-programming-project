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

        private async Task<object> LoginWithEmailAndPasswordAsync(string email, string password)
        {
            var firebase = DependencyService.Get<IFirebaseAuthentication>();
            //var result = await firebase.LoginWithEmailAndPassword(email, password);
            FirebaseLoginRespons respons = await firebase.LoginWithEmailAndPassword("xamarin@testuser.com", "test12345");
            return respons;

        }

        private async void TapGestureRecognizer_TappedAsync(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entEmail.Text) && !string.IsNullOrWhiteSpace(entPassword.Text))
            {
                FirebaseLoginRespons respons = (FirebaseLoginRespons)await LoginWithEmailAndPasswordAsync(entEmail.Text, entPassword.Text);
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