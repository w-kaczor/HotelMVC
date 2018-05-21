using HotelsMVC.DAL;
using HotelsMVC.Models;
using HotelsMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelsMVC.Infrastructure.Hangfire
{
    public static class Emails
    {
        public static void SendHotelAddedEmail(int hotelId)
        {
            HotelsContext db = new HotelsContext();
            var hotel = db.Hotels.Find(hotelId);
            var user = hotel.ApplicationUser;
            HotelAddedEmail email = new HotelAddedEmail();
            email.To = user.Email;
            email.HotelId = hotel.HotelId;
            email.HotelName = hotel.Name;
            email.Send();
        }

        public static void SendHotelVerifiedEmail(int hotelId)
        {
            HotelsContext db = new HotelsContext();
            var hotel = db.Hotels.Find(hotelId);
            var user = hotel.ApplicationUser;
            HotelVerifiedEmail email = new HotelVerifiedEmail();
            email.To = user.Email;
            email.HotelId = hotel.HotelId;
            email.HotelName = hotel.Name;
            email.Send();
        }

        public static void SendHotelHiddenEmail(int hotelId)
        {
            HotelsContext db = new HotelsContext();
            var hotel = db.Hotels.Find(hotelId);
            var user = hotel.ApplicationUser;
            HotelHiddenEmail email = new HotelHiddenEmail();
            email.To = user.Email;
            email.HotelName = hotel.Name;
            email.Send();
        }

        public static void SendHotelDeletedEmail(string userEmail, string hotelName)
        {
            HotelDeletedEmail email = new HotelDeletedEmail();
            email.To = userEmail;
            email.HotelName = hotelName;
            email.Send();
        }

        public static void SendHotelPromotedEmail(int hotelId)
        {
            HotelsContext db = new HotelsContext();
            HotelPromotedEmail email = new HotelPromotedEmail();
            var hotel = db.Hotels.Find(hotelId);
            var user = hotel.ApplicationUser;
            email.To = user.Email;
            email.DateOfPromotion = hotel.DateOfPromotion.GetValueOrDefault();
            email.HotelName = hotel.Name;
            email.Send();
        }
    }
}