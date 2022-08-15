using Project.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface IFirebaseAuthentication
    {
        Task<FirebaseLoginAndSignupRespons> LoginWithEmailAndPassword(string email, string password);
        Task<FirebaseLoginAndSignupRespons> RegisterWithEmailAndPassword(string username, string email, string password);
        FirebaseLoginAndSignupRespons GetCurrentUser();
    }
}
