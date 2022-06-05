using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Areas.Administrator.Data.message
{
    public class XMessage
    {
        public static bool has_flash()
        {
            if (System.Web.HttpContext.Current.Session["Message"].Equals(""))
            {
                return false;
            }
            return true;
        }
        public static void set_flash(String msg, String msg_css)
        {
            ModelThongbao ms = new ModelThongbao();
            ms.msg = msg;
            ms.msg_type = msg_css;
            System.Web.HttpContext.Current.Session["Message"] = ms;
        }
        public static ModelThongbao get_flash()
        {
            ModelThongbao ms = (ModelThongbao)System.Web.HttpContext.Current.Session["Message"];
            System.Web.HttpContext.Current.Session["Message"] = "";
            return ms;
        }
    }
}