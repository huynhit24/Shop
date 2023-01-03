using Shop.ZaloPay;
using Shop.ZaloPay.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shop.ZaloPay.Controllers
{
    public class RedirectController : Controller
    {
        // GET: Redirect
        public async Task<ActionResult> Index()
        {
            bool isValidRedirect;

            try
            {
                var data = new Dictionary<string, object>();
                Request.QueryString.CopyTo(data);

                isValidRedirect = ZaloPayHelper.VerifyRedirect(data);
                if (isValidRedirect)
                {
                    var apptransid = data["apptransid"].ToString();
                    using (var db = new ZaloPayDemoContext())
                    {
                        var orderDTO = db.Orders.First(o => o.Apptransid.Equals(apptransid));
                        if (orderDTO != null && orderDTO.Status == 0)
                        {
                            var orderStatus = await ZaloPayHelper.GetOrderStatus(apptransid);
                            var returncode = int.Parse(orderStatus["returncode"].ToString());

                            orderDTO.Zptransid = orderStatus["zptransid"].ToString();
                            orderDTO.Channel = int.Parse(orderStatus["pmcid"].ToString());
                            orderDTO.Status = returncode == 1 ? 1 : -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isValidRedirect = false;
                ViewData["Exception"] = ex.Message;
            }

            ViewData["isValidRedirect"] = isValidRedirect;

            return View();
        }
    }
}