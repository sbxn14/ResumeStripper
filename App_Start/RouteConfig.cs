﻿using System.Web.Mvc;
using System.Web.Routing;

namespace ResumeStripper
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "CV", action = "Index", id = UrlParameter.Optional }
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
                );
        }
    }
}