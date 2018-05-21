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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HotelsMVC.ViewModels;
using HotelsMVC.Infrastructure.Hangfire;
using Hangfire;
using GoogleMaps.LocationServices;

namespace HotelsMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private HotelsContext db = new HotelsContext();

        // GET: AdminPanel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllHotels()
        {
            var hotels = db.Hotels.Select(x => new
                                        {
                                            HotelId = x.HotelId,
                                            Name = x.Name,
                                            DateAdded = x.DateAdded,
                                            DateOfPromotion = x.DateOfPromotion,
                                            City = x.Address.City,
                                            Street = x.Address.Street,
                                            Phone = x.Address.Phone
                                        });
            var wrapper = new { data = hotels };
            var json = JsonConvert.SerializeObject(wrapper);
            return Content(json);
        }

        public ActionResult HotelsToVerify()
        {
            var hotels = db.Hotels.Where(x => x.IsHidden == true).Select(x => new
                                        {
                                          HotelId = x.HotelId,
                                          Name = x.Name,
                                          DateAdded = x.DateAdded
                                         }).ToList();
            var wrapper = new { data = hotels };
            var json = JsonConvert.SerializeObject(wrapper);
            return Content(json);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Verify(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return RedirectToAction("Index");
            }
            hotel.IsHidden = false;
            db.SaveChanges();
            TempData["Verified"] = id;
            
            BackgroundJob.Enqueue(() => SetCoordinatesHotel(hotel.HotelId));
            BackgroundJob.Enqueue(() =>  Emails.SendHotelVerifiedEmail(hotel.HotelId));
            return RedirectToAction("Index");
        }

        public ActionResult Hide(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return RedirectToAction("Index");
            }
            hotel.IsHidden = true;
            db.SaveChanges();
            TempData["Hidden"] = id;
            BackgroundJob.Enqueue(() => Emails.SendHotelHiddenEmail(hotel.HotelId));
            return RedirectToAction("Index");
        }
        
          public ActionResult Promote(int? id, DateTime date)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return RedirectToAction("Index");
            }
            hotel.DateOfPromotion = date;
            db.SaveChanges();
            TempData["DateOfPromotion"] = date;
            BackgroundJob.Enqueue(() => Emails.SendHotelPromotedEmail(hotel.HotelId));
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return RedirectToAction("Index");
            }
            var userEmail = hotel.ApplicationUser.Email;
            db.Hotels.Remove(hotel);
            db.SaveChanges();
            TempData["Deleted"] = id;
            BackgroundJob.Enqueue(() => Emails.SendHotelDeletedEmail(userEmail, hotel.Name));
            return RedirectToAction("Index");
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region helpers
        public void SetCoordinatesHotel(int hotelId)
        {
            var hotel = db.Hotels.Find(hotelId);
            AddressData loc = new AddressData
            {
                Address = hotel.Address.Street,
                City = hotel.Address.City,
                State = hotel.Address.Voivodeship.Name,
                Country = "Poland",
                Zip = hotel.Address.PostalCode
            };
            var gls = new GoogleLocationService();
            try
            {
                var latlong = gls.GetLatLongFromAddress(loc);
                hotel.Address.Latitude = latlong.Latitude;
                hotel.Address.Longitude = latlong.Longitude;
                db.SaveChanges();
            }
            catch (System.Net.WebException ex)
            {
                BackgroundJob.Enqueue(() => SetCoordinatesHotel(hotel.HotelId));
            } 
        }
        #endregion
    }
}
