using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Home Page",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "About Page",
                url: "gioi-thieu",
                defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Contact Page",
                url: "lien-he",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Advertisement Page",
                url: "quang-cao",
                defaults: new { controller = "Home", action = "QuangCao", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Post Details",
                url: "{controller}/{action}/{id}/{postName}",
                defaults: new { controller = "Home", action = "PostDetails", id = UrlParameter.Optional, postName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Laptop Details",
                url: "{controller}/{action}/{id}/{postName}",
                defaults: new { controller = "Home", action = "Details", id = UrlParameter.Optional, postName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //,namespaces: new[] { "Shop.Controllers" } //thêm namespace phân biệt Area Admin với trang User
            );
        }
    }
}
