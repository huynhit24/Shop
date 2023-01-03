using QRCoder;
using Shop.Models;
using Shop.ZaloPay;
using Shop.ZaloPay.DAL;
using Shop.ZaloPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Shop.Controllers;

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
            ////Lấy giỏ hàng để xử lý
            //DonHang dh = new DonHang();
            //AspNetUser kh = (AspNetUser)Session["TaiKhoan"];// ép session về kh để lấy thông tin
            //Laptop s = new Laptop();
            //GioHangController ghCtrl = new GioHangController();
            //List<GioHang> gh = ghCtrl.Laygiohang();// lấy giỏ hàng

            var amount = "20000";
            var description = Request.Form.Get("description");
            var embeddata = NgrokHelper.CreateEmbeddataWithPublicUrl();
            var bankcode = Request.Form.Get("bankcode");

            var orderData = new OrderData(Convert.ToInt32(amount), description, bankcode, embeddata);
            var order = await ZaloPayHelper.CreateOrder(orderData);

            var returncode = (long)order["returncode"];
            if (returncode == 1)
            {
                /*using (var db = new ZaloPayDemoContext())
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
                }*/

                var orderurl = order["orderurl"].ToString();
                Session["orderurl"] = orderurl;
                Session["QRCodeBase64Image"] = ZaloPay.QRCodeHelper.CreateQRCodeBase64Image(orderurl);
                Session["apptransid"] = orderData.Apptransid;
                Redirect(orderurl);
            }
            else
            {
                Session["error"] = true;
            }

            return Redirect("/trang-chu");
        }
    }
}