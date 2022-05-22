﻿using Newtonsoft.Json.Linq;
using Shop.Mail;
using Shop.Models;
using Shop.MoMo;
using Shop.VnPay.Others;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class GioHangController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();

        public JsonResult GetbyID(int ID)
        {
            HomeModel home = new HomeModel();
            var Laptop = home.GetListChiTietDonHangTheoDonDatHang(ID).Find(x => x.madon.Equals(ID));
            return Json(Laptop, JsonRequestBehavior.AllowGet);
        }

        public List<GioHang> Laygiohang()// lấy ra danh sách sản phẩm trong giỏ hàng
        {
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang == null) // nếu giỏ hàng bằng null thì khởi tạo giỏ hàng rồi gắn giỏ hàng cho Session["Giohang"]
            {
                lstGiohang = new List<GioHang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGioHang(int id, string strURL)// thêm 1 sản phẩm vào giỏ hàng
        {
            List<GioHang> lstGioHang = Laygiohang();
            GioHang sanpham = lstGioHang.Find(n => n.malaptop == id); // tìm sản phẩm đã chọn theo id
            if (sanpham == null)
            {
                sanpham = new GioHang(id);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()// tính tổng số lượng đã mua trong giỏ hàng
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.iSoluong);
            }
            return tsl;

        }
        private int TongSoLuongSanPham()// tính tổng số loại đã chọn mua
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }
        private double TongTien()// tính tổng tiền giỏ hàng
        {
            double tt = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.dThanhTien);
            }
            return tt;

        }
        public ActionResult GioHang()// request trả về View giỏ hàng
        {
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        public ActionResult GioHangPartial()// request trả về partialview giỏ hàng
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }
        public ActionResult XoaGiohang(int id)// xóa sản phẩm theo id
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.malaptop == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.malaptop == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(int id, FormCollection collection)// cập nhật giỏ hàng theo id và form có số lượng
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.malaptop == id);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(collection["txtSolg"].ToString());

            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()// xóa tất cả các mặt hàng trong giỏ hàng
        {
            List<GioHang> lstGioHang = Laygiohang();
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()// đặt hàng
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            DonHang dh = new DonHang();
            AspNetUser kh = (AspNetUser)Session["TaiKhoan"];// ép session về kh để lấy thông tin
            Laptop s = new Laptop();
            List<GioHang> gh = Laygiohang();// lấy giỏ hàng
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);//lấy ngày giao format lại

            dh.makh = kh.Id;
            dh.ngaydat = DateTime.Now;
            dh.ngaygiao = DateTime.Parse(ngaygiao);
            dh.giaohang = false;
            dh.thanhtoan = false;
            /*if ((bool)Session["thanhtoan"] == true)
            {
                dh.thanhtoan = true;
            }
            else
            {
                dh.thanhtoan = false;
            }*/


            data.DonHangs.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.madon = dh.madon;
                ctdh.malaptop = item.malaptop;
                ctdh.soluong = item.iSoluong;
                ctdh.dongia = (decimal)item.giaban;
                s = data.Laptops.Single(n => n.malaptop == item.malaptop);
                data.SubmitChanges();
                data.ChiTietDonHangs.InsertOnSubmit(ctdh);
            }

            //Gửi mail tới khác dùng

            /*string detail = "";

            foreach (var item in gh)
            {
                detail += "Tài khoản:  " + kh.Email.ToString() + "------" + "Mật khẩu:  " + kh.PasswordHash + "=======================";
            }*/

            string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/neworder.html"));

            var total = gh.Sum(n => n.giaban);
            content = content.Replace("{CustomerName}", kh.hoten);
            content = content.Replace("{Phone}", kh.PhoneNumber);
            content = content.Replace("{Email}", kh.Email);
            content = content.Replace("{Total}", total.ToString());

            //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();


            new MailHelper().SendEmail(kh.Email, "Xác nhận đặt mua laptop tại iLaptop", content);
            //new MailHelper().SendEmail(toEmail, "Xác nhận đặt mua laptop tại iLaptop", content);

            //End

            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacnhanDonhang", "GioHang");
        }
        public ActionResult XacnhanDonhang()//xác nhận đơn mạng
        {
            return View();
        }

        public ActionResult XacnhanThanhToan_MoMo()//xác nhận đơn mạng
        {
            return View();
        }

        public ActionResult ThanhToanThatBai()
        {
            return View();
        }

        public ActionResult BadRequestMoMo()
        {
            return View();
        }
        //Thực hiện thanh toán Momo

        /*[HttpGet]
        public ActionResult PaymentMoMo()// đặt hàng
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return RedirectToAction("DatHang","GioHang");
        }

        [HttpPost]*/
        public ActionResult PaymentMoMo()
        {
            DonHang dh = new DonHang();
            AspNetUser kh = (AspNetUser)Session["TaiKhoan"];// ép session về kh để lấy thông tin
            Laptop s = new Laptop();
            List<GioHang> gh = Laygiohang();// lấy giỏ hàng
                                            // var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);//lấy ngày giao format lại

            //dh.makh = kh.Id;
            //dh.ngaydat = DateTime.Now;
            //dh.ngaygiao = DateTime.Now;
            //dh.giaohang = false;
            //dh.thanhtoan = true;
            /*if ((bool)Session["thanhtoan"] == true)
            {
                dh.thanhtoan = true;
            }
            else
            {
                dh.thanhtoan = false;
            }*/


            //data.DonHangs.InsertOnSubmit(dh);
            //data.SubmitChanges();
            //foreach (var item in gh)
            //{
            //    ChiTietDonHang ctdh = new ChiTietDonHang();
            //    ctdh.madon = dh.madon;
            //    ctdh.malaptop = item.malaptop;
            //    ctdh.soluong = item.iSoluong;
            //    ctdh.dongia = (decimal)item.giaban;
            //    s = data.Laptops.Single(n => n.malaptop == item.malaptop);
            //    data.SubmitChanges();
            //    data.ChiTietDonHangs.InsertOnSubmit(ctdh);
            //}

            //Gửi mail tới khác dùng

            /*string detail = "";

            foreach (var item in gh)
            {
                detail += "Tài khoản:  " + kh.Email.ToString() + "------" + "Mật khẩu:  " + kh.PasswordHash + "=======================";
            }*/

            //string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/neworder.html"));

            //var total = gh.Sum(n => n.giaban);
            //content = content.Replace("{CustomerName}", kh.hoten);
            //content = content.Replace("{Phone}", kh.PhoneNumber);
            //content = content.Replace("{Email}", kh.Email);
            //content = content.Replace("{Total}", total.ToString());

            //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();


            //new MailHelper().SendEmail(kh.Email, "Xác nhận đặt mua laptop tại iLaptop", content);
            //new MailHelper().SendEmail(toEmail, "Xác nhận đặt mua laptop tại iLaptop", content);

            //End

            //data.SubmitChanges();
            //Session["GioHang"] = null;
            //return RedirectToAction("XacnhanDonhang", "GioHang");


            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMO5PB020220322";
            string accessKey = "imYC24phv0gYMFgA";
            string serectkey = "gZ2H5gyDOrVLQ0mnVJjPCWQ4a2lenHLN";
            string orderInfo = "Thanh toán mua Laptop";

            //HTTPGET chỉ hiện thông báo người dùng
            string returnUrl = "https://localhost:44381/GioHang/ReturnUrl";

            //HTTPPOST cập nhật database
            string notifyurl = "https://localhost:44381/GioHang/ReturnMoMo"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = gh.Sum(p => p.dThanhTien).ToString();
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i

        public ActionResult ReturnUrl()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = Server.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            string secretkey = ConfigurationManager.AppSettings["serectkey"].ToString();
            //string secretkey = "gZ2H5gyDOrVLQ0mnVJjPCWQ4a2lenHLN";
            string signature = crypto.signSHA256(param, secretkey);
            if (signature != Request["signature"].ToString())
            {
                ViewBag.message = "Thông tin request không hợp lệ";
                return View("BadRequestMoMo");
            }
            if (Request.QueryString["errorCode"].Equals("0"))
            {
                ViewBag.message = "Thanh toán thành công";
                return View("ComfirmPaymentClient");
            }
            else
            {
                ViewBag.message = "Thanh toán không thành công";
                return View("ThanhToanThatBai");
            }
        }

        public ActionResult ReturnMoMo()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = Server.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            //string secretkey = ConfigurationManager.AppSettings["serectkey"].ToString();
            string secretkey = "gZ2H5gyDOrVLQ0mnVJjPCWQ4a2lenHLN";
            string signature = crypto.signSHA256(param, secretkey);
            if (signature != Request["signature"].ToString())
            {
                return View("BadRequestMoMo");
            }
            if (Request.QueryString["errorCode"].Equals("0"))
            {
                SavePayment();
                return View("ComfirmPaymentClient");
            }
            else
            {
                return View("ThanhToanThatBai");
            }
        }

        public ActionResult ConfirmPaymentClient()
        {
            //hiển thị thông báo cho người dùng
            return View();
        }

        [HttpPost]
        public void SavePayment()
        {
            //cập nhật dữ liệu vào db

            DonHang dh = new DonHang();
            AspNetUser kh = (AspNetUser)Session["TaiKhoan"];// ép session về kh để lấy thông tin
            Laptop s = new Laptop();
            List<GioHang> gh = Laygiohang();// lấy giỏ hàng

            dh.makh = kh.Id;
            dh.ngaydat = DateTime.Now;
            dh.ngaygiao = DateTime.Now;
            dh.giaohang = false;
            dh.thanhtoan = true;

            data.DonHangs.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.madon = dh.madon;
                ctdh.malaptop = item.malaptop;
                ctdh.soluong = item.iSoluong;
                ctdh.dongia = (decimal)item.giaban;
                s = data.Laptops.Single(n => n.malaptop == item.malaptop);
                data.SubmitChanges();
                data.ChiTietDonHangs.InsertOnSubmit(ctdh);
            }

            string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/neworder.html"));

            var total = gh.Sum(n => n.giaban);
            content = content.Replace("{CustomerName}", kh.hoten);
            content = content.Replace("{Phone}", kh.PhoneNumber);
            content = content.Replace("{Email}", kh.Email);
            content = content.Replace("{Total}", total.ToString());

            new MailHelper().SendEmail(kh.Email, "Xác nhận đặt mua laptop tại iLaptop", content);
            new MailHelper().SendEmail("ilaptoppro@gmail.com", "Xác nhận đặt mua laptop tại iLaptop", content);

            data.SubmitChanges();
        }

        /*Thanh toán VNPAY && ZaloPay*/

        //Thanh toán VNPAY
        public ActionResult Payment()
        {
            DonHang dh = new DonHang();
            AspNetUser kh = (AspNetUser)Session["TaiKhoan"];// ép session về kh để lấy thông tin
            Laptop s = new Laptop();
            List<GioHang> gh = Laygiohang();// lấy giỏ hàng

            //string url = ConfigurationManager.AppSettings["Url"];
            //string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            //string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            //string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            string url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string returnUrl = "https://localhost:44381/GioHang/XacnhanDonhang";
            string tmnCode = "RFRCD0FS";
            string hashSecret = "CCSNJNJWPZAHKOAWKPZSSPGRHMETMIPP";

            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.0.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", gh.Sum(p => p.dThanhTien).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }

        //Xác thực thanh toán VNPAY
        /*public ActionResult PaymentConfirm()
        {
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return View();
        }*/
    }
}