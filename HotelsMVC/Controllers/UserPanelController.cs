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
using HotelsMVC.ViewModels;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Hangfire;
using HotelsMVC.Infrastructure.Hangfire;
using System.Web.Security;

namespace HotelsMVC.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class UserPanelController : Controller
    {
        private HotelsContext db = new HotelsContext();

        // GET: UserPanel
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var hotels = db.Hotels.Include(h => h.Address).Include(h => h.HotelType).Include(h => h.Region).Where(h => h.ApplicationUser.Id == userId);
            return View(hotels.ToList());
        }

        // GET: UserPanel/Create
        public ActionResult Create()
        {
            ViewBag.HotelTypeId = new SelectList(db.HotelTypes, "HotelTypeId", "Name");
            ViewBag.VoivodeshipId = new SelectList(db.Voivodeships, "VoivodeshipId", "Name");
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name");
            return View();
        }

        // POST: UserPanel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HotelViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.Single(x => x.Id == currentUserId);
                Hotel hotel = new Hotel();
                Address address = new Address();
                hotel.ApplicationUser = currentUser; 
                hotel.Name = vm.Name;
                address.City = vm.City;
                address.Street = vm.Street;
                address.PostalCode = vm.PostalCode;
                address.VoivodeshipId = vm.VoivodeshipId;
                address.Phone = vm.Phone;
                address.Website = vm.Website;
                hotel.Address = address;
                hotel.RegionId = vm.RegionId;
                hotel.HotelTypeId = vm.HotelTypeId;
                hotel.Description = vm.Description;
                hotel.PricePerDayFrom = vm.PricePerDayFrom;
                hotel.PricePerDayTo = vm.PricePerDayTo;
                hotel.DateAdded = DateTime.UtcNow;
                hotel.IsHidden = true;

                WebImage thumbnail = new WebImage(vm.Thumbnail.InputStream);
                thumbnail.Resize(800, 450);
                var thumbnailFileName = Guid.NewGuid() + "-logo";
                var thumbnailFullPath = System.IO.Path.Combine(Server.MapPath("~/Photos"), thumbnailFileName);
                thumbnail.Save(thumbnailFullPath, "jpeg", true);
                hotel.ThumbnailPath = thumbnailFileName + ".jpeg";

                hotel.Gallery = new List<Photo>();
                foreach (var item in vm.Gallery)
                {
                    WebImage img = new WebImage(item.InputStream);
                    img.Resize(800, 450);
                    var fileName = Guid.NewGuid().ToString();
                    string fullPath = System.IO.Path.Combine(Server.MapPath("~/Photos"), fileName);
                    img.Save(fullPath, "jpeg", true);
                    hotel.Gallery.Add(new Photo() { Path = fileName + ".jpeg" });
                }

                db.Hotels.Add(hotel);
                db.SaveChanges();
                TempData["Added"] = vm.Name;
                var userEmail = db.Users.Where(x => x.Id == currentUserId).Select(x => x.Email).Single();
                BackgroundJob.Enqueue(() => Emails.SendHotelAddedEmail(hotel.HotelId));
                return RedirectToAction("Index");

            }

            ViewBag.HotelTypeId = new SelectList(db.HotelTypes, "HotelTypeId", "Name");
            ViewBag.VoivodeshipId = new SelectList(db.Voivodeships, "VoivodeshipId", "Name");
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name");
            return View(vm);
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
            var userId = User.Identity.GetUserId();
            var hotels = db.Hotels.Where(h => h.ApplicationUser.Id == userId).ToList();
            if (!hotels.Contains(hotel))
            {
                return RedirectToAction("Index");
            }
            db.Hotels.Remove(hotel);
            db.SaveChanges();
            TempData["Deleted"] = hotel.Name;
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
