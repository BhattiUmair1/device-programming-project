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

[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthentication))]
namespace Project.Droid.Firebase
{
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        private FirebaseLoginRespons _FirebaseLoginRespons { get; set; }
        public FirebaseAuthentication()
        {
            _FirebaseLoginRespons = new FirebaseLoginRespons();
        }
        public async Task<FirebaseLoginRespons> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.GetInstance(FirestoreService.app).SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);

                _FirebaseLoginRespons.DisplayName = user.User.DisplayName;
                _FirebaseLoginRespons.Uid = user.User.Uid;
                _FirebaseLoginRespons.PhotoUri = (string)user.User.PhotoUrl;
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
        public bool IsSignIn()
        {
            var user = FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }
        public bool SignOut()
        {
            try
            {
                FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}