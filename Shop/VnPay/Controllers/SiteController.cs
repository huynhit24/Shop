using QRCoder;
using Shop.ZaloPay;
using Shop.ZaloPay.DAL;
using Shop.ZaloPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shop.ZaloPay.Controllers
{
    public class SiteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var amount = long.Parse(Request.Form.Get("amount"));
            var description = Request.Form.Get("description");
            var embeddata = NgrokHelper.CreateEmbeddataWithPublicUrl();
            var bankcode = Request.Form.Get("bankcode");

            var orderData = new OrderData(amount, description, bankcode, embeddata);
            var order = await ZaloPayHelper.CreateOrder(orderData);

            var returncode = (long)order["returncode"];
            if (returncode == 1)
            {
                using (var db = new ZaloPayDemoContext())
                {
                    db.Orders.Add(new Models.Order
                    {
                        Apptransid = orderData.Apptransid,
                        Amount = orderData.Amount,
                        Timestamp = orderData.Apptime,
                        Description = orderData.Description,
                        Status = 0
                    });

                    db.SaveChanges();
                }

                var orderurl = order["orderurl"].ToString();
                Session["orderurl"] = orderurl;
                Session["QRCodeBase64Image"] = ZaloPay.QRCodeHelper.CreateQRCodeBase64Image(orderurl);
                Session["apptransid"] = orderData.Apptransid;
            }
            else
            {
                Session["error"] = true;
            }

            return Redirect("/");
        }
    }
}