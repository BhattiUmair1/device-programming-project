using Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Interfaces
{
    public interface IFirebaseUserService
    {
        public FirebaseUserInfo GetUser();
    }
}
