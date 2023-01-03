using Shop.ZaloPay.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.ZaloPay.Controllers
{
    public class HistoryController : Controller
    {
        private readonly int orderPerPage = 10;
        // GET: History
        public ActionResult Index()
        {
            int page;

            try
            {
                page = int.Parse(Request.QueryString["page"]);
            }
            catch
            {
                page = 1;
            }

            using (var db = new ZaloPayDemoContext())
            {
                var ors = db.Orders
                    .GroupJoin(
                        db.Refunds,
                        order => order.Zptransid,
                        refund => refund.Zptransid,
                        (order, refunds) => new { Order = order, Refunds = refunds })
                    .SelectMany(
                        o => o.Refunds.DefaultIfEmpty(),
                        (order, refund) => new { order.Order, Refund = refund })
                    .OrderByDescending(o => o.Order.Timestamp)
                    .Skip((page - 1) * orderPerPage)
                    .Take(orderPerPage)
                    .ToList();

                var totalOrder = db.Orders.Count();
                var orders = ors.GroupBy(o => o.Order.Apptransid).Select(g => g.First().Order).ToList();
                var totalRefunds = ors.GroupBy(o => o.Order.Apptransid).Select(g => g.Sum(o => o.Refund != null ? o.Refund.Amount : 0)).ToList();

                ViewData["orders"] = orders;
                ViewData["totalRefunds"] = totalRefunds;
                ViewData["totalOrder"] = totalOrder;
                ViewData["orderPerPage"] = orderPerPage;
                ViewData["page"] = page;
            }

            return View();
        }
    }
}