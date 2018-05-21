using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelsMVC.Models
{
    public class Voivodeship
    {
        public int VoivodeshipId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Address> Hotels { get; set; }
    }
}