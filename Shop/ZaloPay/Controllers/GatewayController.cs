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
    public class GatewayController : Controller
    {
        // GET: Gateway
        public async Task<ActionResult> Index()
        {
            var banklistResponse = await ZaloPayHelper.GetBankList();
            var banklist = ZaloPayHelper.ParseBankList(banklistResponse);

            ViewData["banklist"] = banklist;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var amount = long.Parse(Request.Form.Get("amount"));
            var description = Request.Form.Get("description");
            var bankcode = Request.Form.Get("bankcode");
            var embeddata = NgrokHelper.CreateEmbeddataWithPublicUrl();

            if (bankcode.Equals("ATM"))
            {
                embeddata["bankgroup"] = "ATM";
                bankcode = "";
            }

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

                return Redirect(order["orderurl"].ToString());
            }
            else
            {
                Session["error"] = true;
                return Redirect("/Gateway");
            }
        }
    }
}