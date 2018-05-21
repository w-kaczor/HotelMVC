using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelsMVC.Models
{
    public class HotelType
    {
        public int HotelTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}