using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelsMVC.ViewModels
{
    public class HotelVerifiedEmail : Email
    {
        public string To { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
    }
}