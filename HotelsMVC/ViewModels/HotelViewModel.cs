using HotelsMVC.Infrastructure.Validators;
using HotelsMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelsMVC.ViewModels
{
    public class HotelViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int HotelId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Street { get; set; }

        [Display(Name = "Postal Code")]
        [Required]
        [RegularExpression("[0-9]{2}-[0-9]{3}", ErrorMessage = "{0} must be in format xx-xxx.")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Voivodeship")]
        public int VoivodeshipId { get; set; }

        [Required]
        [RegularExpression("[0-9]{9}", ErrorMessage = "{0} must be in format xxxxxxxxx")]
        public string Phone { get; set; }

        [Url]
        public string Website { get; set; }

        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int HotelTypeId { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }

        [Display(Name = "Minimum price per day")]
        [Required]
        [Range(10, 1000)]
        public decimal PricePerDayFrom { get; set; }

        [Display(Name = "Maximum price per day")]
        [Required]
        [Range(10, 1000)]
        public decimal PricePerDayTo { get; set; }

        [FileValidator(2, new string[] { ".png", ".jpg", ".jpeg" }, ErrorMessage = "Please choose a PNG, JPG or JPEG image smaller than 2 MB.")]
        public HttpPostedFileBase Thumbnail { get; set; }

        [FilesValidator(5, 10, new string[] { ".png", ".jpg", ".jpeg" }, ErrorMessage = "Please choose at most 5 PNG, JPG or JPEG images. The size of photos cannot exceed 10 MB.")]
        public IEnumerable<HttpPostedFileBase> Gallery { get; set; }
    }
}