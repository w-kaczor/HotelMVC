using HotelsMVC.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelsMVC.Controllers
{
    public class MapController : Controller
    {
        private HotelsContext db = new HotelsContext();
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllHotels()
        {
            var hotels = db.Hotels.Select(x => new
            {
                Name = x.Name,
                City = x.Address.City,
                Street = x.Address.Street,
                PostalCode = x.Address.PostalCode,
                Phone = x.Address.Phone,
                Latitude = x.Address.Latitude,
                Longitude = x.Address.Longitude
            });
            var json = JsonConvert.SerializeObject(hotels);
            return Content(json);
        }
    }
}