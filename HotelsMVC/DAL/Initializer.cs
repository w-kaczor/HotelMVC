using HotelsMVC.Migrations;
using HotelsMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace HotelsMVC.DAL
{
    public class Initializer : DropCreateDatabaseIfModelChanges<HotelsContext>
    {
        protected override void Seed(HotelsContext context)
        {
            SeedStoreData(context);
            base.Seed(context);
        }

        public static void SeedStoreData(HotelsContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var adminRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                adminRole.Name = "Admin";
                roleManager.Create(adminRole);
                var admin = new ApplicationUser();
                admin.Email = "admin@gmail.com";
                admin.UserName = "admin@gmail.com";
                string userPw = "admin123";
                var chkAdmin = UserManager.Create(admin, userPw);
                if (chkAdmin.Succeeded)
                {
                    var result = UserManager.AddToRole(admin.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("User"))
            {
                var userRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                userRole.Name = "User";
                roleManager.Create(userRole);
                List<ApplicationUser> users = new List<ApplicationUser>()
                {
                    new ApplicationUser(){Email = "user1@gmail.com", UserName = "user1@gmail.com"},
                    new ApplicationUser(){Email = "user2@gmail.com", UserName = "user2@gmail.com"},
                    new ApplicationUser(){Email = "user3@gmail.com", UserName = "user3@gmail.com"},
                    new ApplicationUser(){Email = "user4@gmail.com", UserName = "user4@gmail.com"},
                    new ApplicationUser(){Email = "user5@gmail.com", UserName = "user5@gmail.com"},
                    new ApplicationUser(){Email = "user6@gmail.com", UserName = "user6@gmail.com"},
                };
                string userPw = "user123";
                foreach (var item in users)
                {
                    var chkUser = UserManager.Create(item, userPw);
                    if (chkUser.Succeeded)
                    {
                        var result = UserManager.AddToRole(item.Id, "User");
                    }
                }
            }
            
            var regions = new List<Region>
            {
               new Region(){RegionId = 1, Name = "By the sea"},
               new Region(){RegionId = 2, Name = "By the lake"},
               new Region(){RegionId = 3, Name = "In the mountains"}
            };

            var hotelTypes = new List<HotelType>
            {
                new HotelType(){Name = "Apartament"},
                new HotelType(){Name = "Campsite"},
                new HotelType(){Name = "Guesthouse"},
                new HotelType(){Name = "Summer house"},
                new HotelType(){Name = "Villa"},
            };
            regions.ForEach(x => context.Regions.AddOrUpdate(x));

            var voivodeships = new List<Voivodeship>
            {
               new Voivodeship(){VoivodeshipId = 1, Name = "Greater Poland"},
               new Voivodeship(){VoivodeshipId = 2, Name = "Kuyavian-Pomeranian"},
               new Voivodeship(){VoivodeshipId = 3, Name = "Lesser Poland"},
               new Voivodeship(){VoivodeshipId = 4, Name = "Łódź"},
               new Voivodeship(){VoivodeshipId = 5, Name = "Lower Silesian"},
               new Voivodeship(){VoivodeshipId = 6, Name = "Lublin"},
               new Voivodeship(){VoivodeshipId = 7, Name = "Lubusz"},
               new Voivodeship(){VoivodeshipId = 8, Name = "Masovian"},
               new Voivodeship(){VoivodeshipId = 9, Name = "Opole"},
               new Voivodeship(){VoivodeshipId = 10, Name = "Podlaskie"},
               new Voivodeship(){VoivodeshipId = 11, Name = "Pomeranian"},
               new Voivodeship(){VoivodeshipId = 12, Name = "Silesian"},
               new Voivodeship(){VoivodeshipId = 13, Name = "Subcarpathian"},
               new Voivodeship(){VoivodeshipId = 14, Name = "Świętokrzyskie"},
               new Voivodeship(){VoivodeshipId = 15, Name = "Warmian-Masurian"},
               new Voivodeship(){VoivodeshipId = 16, Name = "West Pomeranian"},
            };
             
            voivodeships.ForEach(x => context.Voivodeships.AddOrUpdate(x));
           
            Random random = new Random();
            var hotels = new List<Hotel>
            {
               new Hotel(){HotelId = 1, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 1", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = DateTime.UtcNow.AddDays(random.Next(5,70)), ThumbnailPath = "photo1.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }},  Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Balice", Street = "Kapitana Mieczysława Medweckiego 1", PostalCode = "32-083", Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.071206, Longitude = 19.801143, Phone = "333222111", Website = "https://www.google.pl/" }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
               new Hotel(){HotelId = 2, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 2", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = DateTime.UtcNow.AddDays(random.Next(5,70)), ThumbnailPath = "photo2.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }},  Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Kryspinów", Street = "Kryspinów 11", PostalCode = "32-060", Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.043099, Longitude = 19.795193, Phone = "333222111", Website = "https://www.google.pl/"  }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
               new Hotel(){HotelId = 3, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 3", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = DateTime.UtcNow.AddDays(random.Next(5,70)), ThumbnailPath = "photo3.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }},  Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Kryspinów", Street = "Kryspinów 22", PostalCode = "32-060", Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.042796, Longitude = 19.798187, Phone = "333222111", Website = "https://www.google.pl/"  }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
               new Hotel(){HotelId = 4, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 4", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = null, ThumbnailPath = "photo4.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }},  Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Cholerzyn", Street = "Cholerzyn 66", PostalCode = "32-060" ,Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.057646, Longitude = 19.761350, Phone = "333222111", Website = "https://www.google.pl/" }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
               new Hotel(){HotelId = 5, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 5", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = null, ThumbnailPath = "photo5.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }},  Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Cholerzyn", Street = "Cholerzyn 77", PostalCode = "32-060" ,Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.058108, Longitude = 19.754758, Phone = "333222111", Website = "https://www.google.pl/" }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
               new Hotel(){HotelId = 6, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 6", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = DateTime.UtcNow.AddDays(random.Next(5,70)), ThumbnailPath = "photo6.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }},  Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Cholerzyn", Street = "Cholerzyn 88", PostalCode = "32-060" ,Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.058025, Longitude = 19.763261, Phone = "333222111", Website = "https://www.google.pl/" }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
               new Hotel(){HotelId = 7, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 7", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = DateTime.UtcNow.AddDays(random.Next(5,70)), ThumbnailPath = "photo1.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }},  Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Cholerzyn", Street = "Cholerzyn 205", PostalCode = "32-060" ,Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.056853, Longitude = 19.773230, Phone = "333222111", Website = "https://www.google.pl/" }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
               new Hotel(){HotelId = 8, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 8", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = DateTime.UtcNow.AddDays(random.Next(5,70)), ThumbnailPath = "photo2.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }},  Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Liszki", Street = "Liszki 11", PostalCode = "32-060" ,Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.039026, Longitude = 19.765555, Phone = "333222111", Website = "https://www.google.pl/" }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
               new Hotel(){HotelId = 9, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 9", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = DateTime.UtcNow.AddDays(random.Next(5,70)), ThumbnailPath = "photo3.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }},  Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Liszki", Street = "Liszki 22", PostalCode = "32-060" ,Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.039724, Longitude = 19.766342, Phone = "333222111", Website = "https://www.google.pl/" }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
               new Hotel(){HotelId = 10, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 10", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = null, ThumbnailPath = "photo4.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }}, Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Liszki", Street = "Liszki 44", PostalCode = "32-060",Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.038852, Longitude = 19.769599, Phone = "333222111", Website = "https://www.google.pl/"  }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
               new Hotel(){HotelId = 11, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 11", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = null, ThumbnailPath = "photo5.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }}, Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Liszki", Street = "Liszki 469", PostalCode = "32-060",Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.041424, Longitude = 19.783904, Phone = "333222111", Website = "https://www.google.pl/"  }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
               new Hotel(){HotelId = 12, ApplicationUser = context.Users.OrderBy(x => Guid.NewGuid()).First(), Name  = "Hotel 12", PricePerDayFrom = random.Next(10, 50) , PricePerDayTo = random.Next(60, 150), IsHidden = false, DateAdded = DateTime.UtcNow, DateOfPromotion = DateTime.UtcNow.AddDays(random.Next(5,70)), ThumbnailPath = "photo6.jpeg", Gallery = new List<Photo>() { new Photo(){ Path = "photo1.jpeg"}, new Photo() { Path = "photo2.jpeg" }, new Photo() { Path = "photo3.jpeg" }, new Photo() { Path = "photo4.jpeg" }}, Region = regions.OrderBy(x => Guid.NewGuid()).First(),HotelType = hotelTypes.OrderBy(x => Guid.NewGuid()).First(), Address = new Address(){ City = "Liszki", Street = "Liszki 427", PostalCode = "32-060",Voivodeship = voivodeships.Where(x => x.Name == "Lesser Poland").Single(), Latitude = 50.037418, Longitude = 19.764955, Phone = "333222111", Website = "https://www.google.pl/"  }, Description = "Velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget sem nulla eu ultricies orci praesent id augue nec lorem pretium congue sit amet ac nunc fusce iaculis lorem eu diam hendrerit at mattis purus dignissim vivamus mauris tellus, fringilla vel dapibus a, blandit quis erat vivamus elementum aliquam luctus etiam fringilla pretium sem vitae sodales mauris id nulla est praesent laoreet, metus vel auctor aliquam, eros purus vulputate leo, eget consequat neque quam id tellus duis ultricies tempor tortor, vitae dignissim ligula mattis nec in hac habitasse platea dictumst." },
            };
            hotels.ForEach(x => context.Hotels.AddOrUpdate(x));
            context.SaveChanges();
        }
    }
}


