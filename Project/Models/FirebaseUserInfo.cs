using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Models
{
    public class FirebaseUserInfo
    {
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public bool? IsAnonymous { get; set; }
        public bool? IsEmailVerified { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhotoUrl { get; set; }
        //public int ProviderData { get; set; }
        //public int ProviderId { get; set; }
        //public int Providers { get; set; }
        public string? Uid { get; set; }
    }
}
