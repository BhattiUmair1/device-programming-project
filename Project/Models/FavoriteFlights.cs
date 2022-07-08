using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project.Models
{
    public class UserInfo
    {
        public string UserID { get; set; }
        public List<Flights> Flights { get; set; }
    }
}
