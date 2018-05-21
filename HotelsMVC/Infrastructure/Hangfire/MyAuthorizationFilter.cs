using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire.Annotations;
using Microsoft.Owin;

namespace HotelsMVC.Infrastructure.Hangfire
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            //// In case you need an OWIN context, use the next line, `OwinContext` class
            //// is the part of the `Microsoft.Owin` package.
            var owinContext = new OwinContext(context.GetOwinEnvironment());

            //// Allow all authenticated users to see the Dashboard (potentially dangerous).
            ////return owinContext.Authentication.User.Identity.IsAuthenticated;
            return owinContext.Request.User.IsInRole("Admin");    
        }
    }
}