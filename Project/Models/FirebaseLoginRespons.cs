using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Models
{
    public class FirebaseLoginAndSignupRespons
    {
        public string? DisplayName { get; set; }
        public string? Uid { get; set; }
        public string? PhotoUri { get; set; }
        public string? Token { get; set; }
        public bool IsError { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
