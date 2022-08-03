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
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
            Loadicons();
        }

        private void Loadicons()
        {
            imgbackground.Source = ImageSource.FromResource("Project.Assets.Background.png");
        }
        private async void TapGestureRecognizer_GoToSigninPage(object sender, EventArgs e)
        {
            Label labelBtnSignInPage = sender as Label;
            labelBtnSignInPage.Opacity = 0.5;
            await Task.Delay(200);
            labelBtnSignInPage.Opacity = 1;
            await Navigation.PushAsync(new LoginPage());
        }

        private async void LoginWithEmailAndPasswordAsync(string username, string email, string password)
        {
            var firebase = DependencyService.Get<IFirebaseAuthentication>();
            FirebaseLoginAndSignupRespons respons = await firebase.RegisterWithEmailAndPassword(username, email, password);
            if (respons.IsError)
            {
                frameErrorBox.IsVisible = true;
                lblErrorMessage.Text = respons.ErrorMessage;
            } else
            {
                await Navigation.PushAsync(new LoginPage());
            }
        }

        private async void TapGestureRecognizer_SignUpBtn(object sender, EventArgs e)
        {
            frameErrorBox.IsVisible = false;
            Frame labelSignUpBtn = sender as Frame;
            labelSignUpBtn.Opacity = 0.7;
            await Task.Delay(200);
            labelSignUpBtn.Opacity = 1;
            if (!string.IsNullOrEmpty(entUsername.Text) && !string.IsNullOrEmpty(entEmail.Text) && !string.IsNullOrEmpty(entPassword.Text))
            {
                LoginWithEmailAndPasswordAsync(entUsername.Text, entEmail.Text, entPassword.Text);
            }
        }
    }
}