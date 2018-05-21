using HotelsMVC.DAL;
using HotelsMVC.Models;
using HotelsMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelsMVC.Controllers
{
    public class HomeController : Controller
    {
        private HotelsContext db = new HotelsContext();
        // GET: Home
        public ActionResult Index()
        {
            var promotedHotels = db.Hotels.Where(x => x.DateOfPromotion > DateTime.UtcNow && x.IsHidden == false)
                                    .OrderBy(x => Guid.NewGuid())
                                    .ToList();

            return View(promotedHotels);
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}