using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using FirestoreRe.Droid.Services;
using Project.Droid.Firebase;
using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthenticationService))]
namespace Project.Droid.Firebase
{
    public class FirebaseAuthenticationService : IFirebaseAuthentication
    {
        private FirebaseLoginAndSignupRespons _FirebaseLoginRespons { get; set; }
        public FirebaseAuthenticationService()
        {
            _FirebaseLoginRespons = new FirebaseLoginAndSignupRespons();
        }

        public async Task<FirebaseLoginAndSignupRespons> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.GetInstance(FirestoreService.app).SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);

                _FirebaseLoginRespons.DisplayName = user.User.DisplayName;
                _FirebaseLoginRespons.Uid = user.User.Uid;
                _FirebaseLoginRespons.PhotoUrl = (string)user.User.PhotoUrl;
                _FirebaseLoginRespons.Token = token.Token;
                _FirebaseLoginRespons.IsError = false;
                _FirebaseLoginRespons.ErrorMessage = "";

                return _FirebaseLoginRespons;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                _FirebaseLoginRespons.ErrorMessage = e.GetBaseException().Message;
                _FirebaseLoginRespons.IsError = true;
                return _FirebaseLoginRespons;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                _FirebaseLoginRespons.ErrorMessage = e.GetBaseException().Message;
                _FirebaseLoginRespons.IsError = true;
                return _FirebaseLoginRespons;
            }
        }

        public async Task<FirebaseLoginAndSignupRespons> RegisterWithEmailAndPassword(string username, string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.GetInstance(FirestoreService.app).SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);

                UserProfileChangeRequest userProfileChangeRequest = new UserProfileChangeRequest.Builder().SetDisplayName(username).Build();
                await user.User.UpdateProfileAsync(userProfileChangeRequest);

                _FirebaseLoginRespons.DisplayName = username;
                _FirebaseLoginRespons.Uid = user.User.Uid;
                _FirebaseLoginRespons.PhotoUrl = (string)user.User.PhotoUrl;
                _FirebaseLoginRespons.Token = token.Token;
                _FirebaseLoginRespons.IsError = false;
                _FirebaseLoginRespons.ErrorMessage = "";

                return _FirebaseLoginRespons;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                _FirebaseLoginRespons.ErrorMessage = e.GetBaseException().Message;
                _FirebaseLoginRespons.IsError = true;
                return _FirebaseLoginRespons;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                _FirebaseLoginRespons.ErrorMessage = e.GetBaseException().Message;
                _FirebaseLoginRespons.IsError = true;
                return _FirebaseLoginRespons;
            }
        }

        public FirebaseLoginAndSignupRespons GetCurrentUser()
        {
            return _FirebaseLoginRespons;
        }
    }
}