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
    public class QuickPayController : Controller
    {
        // GET: Quickpay
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var embeddata = NgrokHelper.CreateEmbeddataWithPublicUrl();
            var paymentcodeRaw = Request.Form.Get("paymentcodeRaw");
            var amount = long.Parse(Request.Form.Get("amount"));
            var description = Request.Form.Get("description");

            var orderData = new QuickPayOrderData(amount, paymentcodeRaw, description, embeddata);
            var result = await ZaloPayHelper.QuickPay(orderData);

            var returncode = int.Parse(result["returncode"].ToString());

            if (returncode > 0)
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
            }

            Session["result"] = result;

            return Redirect("/QuickPay/Index");
        }
    }
}