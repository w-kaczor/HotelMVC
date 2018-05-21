using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelsMVC.DAL;
using HotelsMVC.Models;
using PagedList;

namespace HotelsMVC.Controllers
{
    public class SearchController : Controller
    {
        private HotelsContext db = new HotelsContext();
        // GET: Search
        public ActionResult Index(string city, string hotelTypes, string localization, int? priceFrom, int? priceTo, int? page)
        {
            TempData["city"] = city;
            var hotels = db.Hotels.Include(x => x.Address).Where(x => x.IsHidden == false);

            if (!String.IsNullOrEmpty(city))
            {
                 hotels = hotels.Where(x => x.Address.City.Contains(city));
            }
            if (!String.IsNullOrEmpty(hotelTypes))
            {
                hotels = hotels.Where(x => x.HotelType.Name == hotelTypes);
            }
            if (!String.IsNullOrEmpty(localization))
            {
                hotels = hotels.Where(x => x.Region.Name == localization || x.Address.Voivodeship.Name == localization);
            }
            if (priceFrom == null)
            {
                priceFrom = 0;
            }
            if (priceTo == null)
            {
                priceTo = 10000;
            }
            hotels = hotels.Where(x => x.PricePerDayFrom >= priceFrom && x.PricePerDayTo <= priceTo);

            var localizationList = db.Regions.Select(x => x.Name).ToList();
            localizationList.AddRange(db.Voivodeships.Select(x => x.Name));
            ViewBag.hotelTypes = new SelectList(db.HotelTypes.Select(x => x.Name));
            ViewBag.localization = new SelectList(localizationList);

            if (Request.IsAjaxRequest())
            {
                return PartialView("HotelList", hotels.ToList().ToPagedList(page ?? 1, 4));
            }
            else
            {
                return View(hotels.ToList().ToPagedList(page ?? 1, 4));
            }
        }

        public PartialViewResult Search()
        {
            List<string> headerImages = new List<string>()
            {
                "~/Photos/header-photo1.jpeg",
                "~/Photos/header-photo2.jpeg",
                "~/Photos/header-photo3.jpeg"
            };
            var img = headerImages.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            return PartialView("Search", img);
        }

        public ActionResult CityHint(string term)
        {
            var hotels = db.Hotels.Where(x => x.IsHidden == false && x.Address.City.Contains(term)).Take(5).Select(x => new { label = x.Address.City }).Distinct();
            return Json(hotels, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }
    }
}