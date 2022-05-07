using System.Web.Mvc;

namespace Shop.Areas.Administrator
{
    public class AdministratorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
             name: "admin",
             url: "Admin",
             defaults: new { controller = "MainPage", action = "Index", AreaName = "Administrator" }
            );
            context.MapRoute(
             name: "login",
             url: "Admin/Login",
             defaults: new { controller = "MainPage", action = "LoginAdmin", AreaName = "Administrator" }
            );
            context.MapRoute(
             name: "adlogin",
             url: "MainPage/Login",
             defaults: new { controller = "MainPage", action = "LoginAdmin", AreaName = "Administrator" }
            );
            context.MapRoute(
                "Administrator_default",
                "Administrator/{controller}/{action}/{id}",
                new { action = "Index", Controller = "MainPage", id = UrlParameter.Optional }
            );
        }
    }
}