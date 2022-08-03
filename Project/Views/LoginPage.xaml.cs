using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            LoginWithEmailAndPasswordAsync("", "");
        }

        private void Loadicons()
        {
            imgbackground.Source = ImageSource.FromResource("Project.Assets.Background.png");
        }

        private async void LoginWithEmailAndPasswordAsync(string email, string password)
        {
            var firebase = DependencyService.Get<IFirebaseAuthentication>();

            FirebaseLoginAndSignupRespons respons = await firebase.LoginWithEmailAndPassword("xamarin@testuser.com", "test123456");
            //FirebaseLoginAndSignupRespons respons = await firebase.LoginWithEmailAndPassword(email, password);
            if (respons.IsError != true)
            {
                await Navigation.PushAsync(new SearchPage());
            }
            else
            {
                frameErrorBox.IsVisible = true;
                lblErrorMessage.Text = respons.ErrorMessage;
            }
        }


        private async void TapGestureRecognizer_TappedAsync(object sender, EventArgs e)
        {
            frameErrorBox.IsVisible = false;
            Frame frameBtnSignIn = sender as Frame;
            frameBtnSignIn.Opacity = 0.7;
            await Task.Delay(200);
            frameBtnSignIn.Opacity = 1;
            if (!string.IsNullOrWhiteSpace(entEmail.Text) && !string.IsNullOrWhiteSpace(entPassword.Text))
            {
                LoginWithEmailAndPasswordAsync(entEmail.Text, entPassword.Text);
            } else
            {
                frameErrorBox.IsVisible = true;
                lblErrorMessage.Text = "Please fill in all the fields.";
            }
        }

        private async void TapGestureRecognizer_GoToSignUpPage(object sender, EventArgs e)
        {
            Label labelBtnSignUpPage = sender as Label;
            labelBtnSignUpPage.Opacity = 0.5;
            await Task.Delay(200);
            labelBtnSignUpPage.Opacity = 1;
            await Navigation.PushAsync(new SignupPage());
        }
    }
}