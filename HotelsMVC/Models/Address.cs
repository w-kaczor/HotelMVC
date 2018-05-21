using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelsMVC.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public int VoivodeshipId { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Voivodeship Voivodeship { get; set; }  
    }
}