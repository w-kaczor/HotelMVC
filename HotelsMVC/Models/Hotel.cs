using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelsMVC.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RegionId { get; set; }
        public int HotelTypeId { get; set; }
        public decimal PricePerDayFrom { get; set; }
        public decimal PricePerDayTo { get; set; }
        public bool IsHidden { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateOfPromotion { get; set; }
        public string ThumbnailPath { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Address Address { get; set; }
        public virtual Region Region { get; set; }
        public virtual HotelType HotelType { get; set; }
        public virtual ICollection<Photo> Gallery { get; set; }
    }  
}


