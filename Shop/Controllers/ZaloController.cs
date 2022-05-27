using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
// HmacHelper, RSAHelper, HttpHelper, Utils (tải về ở mục DOWNLOADS)
using Shop.ZaloPay;
using Shop.ZaloPay.Crypto;

namespace Shop.Controllers
{
    public class ZaloController : Controller
    {
        //thông tin chung
        /* static string appid = "553";
         static string key1 = "uUfsWgfLkRLzq6W2uNXTCxrfxs51auny";
         static string createOrderUrl = "https://sandbox.zalopay.com.vn/v001/tpe/createorder";*/

        static string appid = "553";
        static string key1 = "9phuAOYhan4urywHTh0ndEXiV3pKHr5Q";
        static string createOrderUrl = "https://sandbox.zalopay.com.vn/v001/tpe/createorder";

        // GET: Zalo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<RedirectResult> ZaloPaymentAsync()
        {
            var transid = Guid.NewGuid().ToString();
            var embeddata = new { merchantinfo = "embeddata123" };
            var items = new[]{
                new { itemid = "knb", itemname = "kim nguyen bao", itemprice = 198400, itemquantity = 1 }
            };
            var param = new Dictionary<string, string>();

            param.Add("appid", appid);
            param.Add("appuser", "demo");
            param.Add("apptime", Util.GetTimeStamp().ToString());
            param.Add("amount", "50000");
            param.Add("apptransid", DateTime.Now.ToString("yyMMdd") + "_" + transid); // mã giao dich có định dạng yyMMdd_xxxx
            param.Add("embeddata", JsonConvert.SerializeObject(embeddata));
            param.Add("item", JsonConvert.SerializeObject(items));
            param.Add("description", "ZaloPay demo");
            param.Add("bankcode", "zalopayapp");

            var data = appid + "|" + param["apptransid"] + "|" + param["appuser"] + "|" + param["amount"] + "|"
                + param["apptime"] + "|" + param["embeddata"] + "|" + param["item"];
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

            var result = await HttpHelper.PostFormAsync(createOrderUrl, param);

            foreach (var entry in result)
            {
                Console.WriteLine("{0} = {1}", entry.Key, entry.Value);
            }
            return Redirect(result.ToString());
        }


    }
}