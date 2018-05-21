namespace HotelsMVC.Migrations
{
    using HotelsMVC.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<HotelsMVC.DAL.HotelsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "HotelsMVC.DAL.HotelsContext";
        }

        protected override void Seed(HotelsMVC.DAL.HotelsContext context)
        {
            Initializer.SeedStoreData(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
