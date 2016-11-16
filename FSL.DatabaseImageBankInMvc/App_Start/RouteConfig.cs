using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FSL.DatabaseImageBankInMvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ImageBank",
                url: GetImageBankRoute() + "/{fileId}/{action}",
                defaults: new { controller = "ImageBank", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        private static string GetImageBankRoute()
        {
            var key = "imagebank:routeName";
            var config = ConfigurationManager.AppSettings.AllKeys.Contains(key) ? ConfigurationManager.AppSettings.Get(key) : "";

            return config ?? "imagebank";
        }
    }
}
