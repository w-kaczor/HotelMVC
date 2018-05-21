using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelsMVC.Models
{
    public class Photo
    {
        public int PhotoId{ get; set; }
        public string Path { get; set; }

        [Required]
        public virtual Hotel Hotel { get; set; }
    }
}