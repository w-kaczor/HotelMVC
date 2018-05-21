using Hangfire;
using Hangfire.Dashboard;
using HotelsMVC.DAL;
using HotelsMVC.Infrastructure.Hangfire;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using System.IO;

[assembly: OwinStartupAttribute(typeof(HotelsMVC.Startup))]
namespace HotelsMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer<HotelsContext>(new Initializer());
            GlobalConfiguration.Configuration.UseSqlServerStorage("HotelsContext");
            app.UseHangfireServer();
            ConfigureAuth(app);
            CreateEmailDir();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new [] {new MyAuthorizationFilter()}
            });
        }

        private void CreateEmailDir()
        {
            string dir = @"c:\EmailsHotelsMvc";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }



   
}
