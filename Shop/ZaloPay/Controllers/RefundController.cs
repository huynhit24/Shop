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
    public class RefundController : Controller
    {
        [HttpPost]
        public async Task Index()
        {
            try
            {
                var data = new Dictionary<string, object>();
                Request.Form.CopyTo(data);

                var amount = long.Parse(Request.Form.Get("amount"));
                var zptransid = Request.Form.Get("zptransid");
                var description = Request.Form.Get("description");

                var refundData = new RefundData(amount, zptransid, description);
                var result = await ZaloPayHelper.Refund(refundData);

                var returncode = int.Parse(result["returncode"].ToString());

                if (returncode >= 1)
                {
                    while (true)
                    {
                        var refundStatus = await ZaloPayHelper.GetRefundStatus(refundData.Mrefundid);
                        var c = int.Parse(refundStatus["returncode"].ToString());

                        if (c < 2)
                        {
                            if (c == 1)
                            {
                                using (var db = new ZaloPayDemoContext())
                                {
                                    db.Refunds.Add(new Refund
                                    {
                                        Amount = refundData.Amount,
                                        Zptransid = refundData.Zptransid,
                                        Mrefundid = refundData.Mrefundid
                                    });

                                    db.SaveChanges();
                                }
                            }

                            Session["refundResult"] = refundStatus;

                            break;
                        }

                        System.Threading.Thread.Sleep(1000);
                    }
                }
                else
                {
                    Session["refundResult"] = result;
                }
            }
            catch (Exception ex)
            {
                var result = new Dictionary<string, object>();
                result["returncode"] = -1;
                result["returnmessage"] = "Exception: " + ex.Message;

                Session["refundResult"] = result;
            }

            Response.Redirect("/History");
        }
    }
}