using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ZaloController : Controller
    {
        //thông tin chung
       /* static string appid = "553";
        static string key1 = "uUfsWgfLkRLzq6W2uNXTCxrfxs51auny";
        static string createOrderUrl = "https://sandbox.zalopay.com.vn/v001/tpe/createorder";*/

        // GET: Zalo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ZaloPayment()
        {
            return View();
        }
    }
}