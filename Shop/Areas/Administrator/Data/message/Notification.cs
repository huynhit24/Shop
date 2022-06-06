using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Areas.Administrator.Data.message
{
    public class Notification
    {
        public static bool has_flash()
        {
            if (HttpContext.Current.Session["Notification"].Equals(""))
            {
                return false;
            }
            return true;
        }
        public static void set_flash(String mgs, String mgs_type)
        {
            ModelNotification tb = new ModelNotification();
            tb.mgs = mgs;
            tb.mgs_type = mgs_type;

            HttpContext.Current.Session["Notification"] = tb;
        }
        public static ModelNotification get_flash()
        {

            ModelNotification Notifi = (ModelNotification)HttpContext.Current.Session["Notification"];
            HttpContext.Current.Session["Notification"] = "";
            return Notifi;
        }
    }
}