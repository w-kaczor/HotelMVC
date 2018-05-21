using HotelsMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HotelsMVC.DAL
{
    public class HotelsContext : IdentityDbContext<ApplicationUser>
    {
        public HotelsContext() : base("HotelsContext")
        {

        }

        public static HotelsContext Create()
        {
            return new HotelsContext();
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelType> HotelTypes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Voivodeship> Voivodeships { get; set; }
        public DbSet<Region> Regions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Hotel>()
                   .HasRequired(a => a.Address)
                   .WithRequiredDependent()
                   .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

    }
}