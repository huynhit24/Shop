using BotDetect.Web.Mvc;
using PagedList;
using Shop.Areas.Administrator.Data.message;
using Shop.Common;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            HomeModel home = new HomeModel();
            return View(home);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult GetChiTietDonHang(int? id)
        {
            List<ChiTietDonHang> list = data.ChiTietDonHangs.Where(n => n.madon == id).ToList();
            return View(list);
        }

        public ActionResult DonHangDaMua()
        {
            if (Session["TaiKhoan"] != null)
            {
                AspNetUser kh = (AspNetUser)Session["TaiKhoan"];
                //AspNetUser ad = (AspNetUser)Session["taikhoanadmin"];
                /*List<DonHang> list = kh.DonHangs.ToList();*/
                List<DonHang> list = data.DonHangs.Where(n => n.makh == kh.Id).ToList();
                if (list == null)
                {
                    return RedirectToAction("GioHang", "GioHang");
                }
                return View(list);
                /*try
                {
                    List<DonHang> list = data.DonHangs.Where(n => n.makh == kh.Id || n.makh == ad.Id).ToList();
                    if (list == null)
                    {
                        return RedirectToAction("GioHang", "GioHang");
                    }
                    return View(list);
                }
                catch (Exception)
                {
                    return RedirectToAction("GioHang", "GioHang");
                }*/
            }
            else
            {
                return RedirectToAction("GioHang", "GioHang");
            }
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCodeID", "contactCaptcha", "Mã Captcha không đúng!")]
        public ActionResult Contact(FormCollection collection, LienHe lh)
        {
            var hoten = collection["hoten"];
            var email = collection["email"];
            var dienthoai = collection["dienthoai"];
            var website = collection["website"];
            var noidung = collection["noidung"];
            var captchaCode = collection["CaptchaCodeID"];
            /*var trangthai = collection["trangthai"];*/
            bool validationContact = hoten == null || noidung == null || hoten.Equals("") || noidung.Equals("");
            if (!ModelState.IsValid)
            {
                if (validationContact)
                {
                    ViewBag.contactContentError = "Bạn chưa điền đủ họ tên và nội dung! 🆘🆘🆘";
                    ModelState.AddModelError("CaptchaCodeID", "Bạn chưa điền đủ thông tin liên hệ (bắt buộc 'Họ tên' && 'Nội dung')!");
                    return RedirectToAction("Contact", "Home");
                }
                if (captchaCode == null || captchaCode.Equals(""))
                {
                    ViewBag.commentContentError = "Bạn chưa điền Captcha! 🆘🆘🆘";
                    ModelState.AddModelError("CaptchaCodeID", "Bạn chưa điền Captcha! 🆘🆘🆘");
                    return RedirectToAction("Contact", "Home");
                }
            }
            else
            {
                lh.hoten = hoten;
                lh.email = email;
                lh.dienthoai = dienthoai;
                lh.website = website;
                lh.noidung = noidung;
                lh.trangthai = true;

                data.LienHes.InsertOnSubmit(lh);
                data.SubmitChanges();
                MvcCaptcha.ResetCaptcha("contactCaptcha");
            }
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult NhanXet()
        {
            return PartialView();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "commentCaptcha", "Mã Captcha không đúng!")]
        public ActionResult NhanXet(FormCollection collection, DanhGia dg)
        {
            var ten = collection["ten"];
            var noidung = collection["noidung"];
            var vote = collection["vote"];
            var malaptop = CommonFields.id;
            var captchaCode = collection["CaptchaCode"];
            /*var trangthai = collection["trangthai"];*/
            bool validationComment = ten == null || noidung == null || vote == null || ten.Equals("") || noidung.Equals("") || vote.Equals("");
            if (!ModelState.IsValid)
            {
                // TODO: Captcha validation failed, show error message
                if (validationComment)
                {
                    ViewBag.commentContentError = "Bạn chưa điền đủ thông tin hoặc chưa vote! 🆘🆘🆘";
                    ModelState.AddModelError("CaptchaCode", "Bạn chưa điền đủ thông tin hoặc chưa vote! 🆘🆘🆘!");
                    return PartialView();
                }
                if (captchaCode == null || captchaCode.Equals(""))
                {
                    ViewBag.commentContentError = "Bạn chưa điền Captcha! 🆘🆘🆘";
                    ModelState.AddModelError("CaptchaCode", "Bạn chưa điền Captcha! 🆘🆘🆘");
                    return PartialView();
                }
            }
            else
            {
                dg.ten = ten;
                dg.noidung = noidung;
                /*dg.vote = Convert.ToInt32(vote);*/
                dg.vote = Convert.ToInt32(vote);
                dg.ngaydanhgia = DateTime.Now;
                dg.malaptop = malaptop;
                dg.trangthai = true;
                data.DanhGias.InsertOnSubmit(dg);
                data.SubmitChanges();
                MvcCaptcha.ResetCaptcha("commentCaptcha");
            }

            /*return RedirectToAction("Details");*/
            return PartialView();
        }

        [HttpGet]
        public ActionResult QuangCao()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCodeAD", "quangcaoCaptcha", "Mã Captcha không đúng!")]
        public ActionResult QuangCao(FormCollection collection, QuangCao qc)
        {
            var tenqc = collection["tenqc"];
            var tencongty = collection["tencongty"];
            var hinhnen = collection["hinhnen"];
            var link = collection["link"];
            var ngaybatdau = String.Format("{0:MM/dd/yyyy}", collection["ngaybatdau"]);
            var ngayhethan = String.Format("{0:MM/dd/yyyy}", collection["ngayhethan"]);
            var captchaCode = collection["CaptchaCodeAD"];

            bool validationQuangcao = tenqc == null || tencongty == null || link == null || ngaybatdau == null || ngayhethan == null || tenqc.Equals("") || tencongty.Equals("") || link.Equals("") || ngaybatdau.Equals("") || ngayhethan.Equals("");
            if (!ModelState.IsValid)
            {
                // TODO: Captcha validation failed, show error message
                if (validationQuangcao)
                {
                    ViewBag.quangcaoContentError = "Bạn chưa điền đủ thông tin liên hệ quảng cáo! 🆘🆘🆘";
                    ModelState.AddModelError("CaptchaCode", "Bạn chưa điền đủ thông tin liên hệ quảng cáo! 🆘🆘🆘!");
                    return RedirectToAction("QuangCao", "Home");
                }
                if (captchaCode == null || captchaCode.Equals(""))
                {
                    ViewBag.quangcaoContentError = "Bạn chưa điền Captcha! 🆘🆘🆘";
                    ModelState.AddModelError("CaptchaCode", "Bạn chưa điền Captcha! 🆘🆘🆘");
                    return RedirectToAction("QuangCao", "Home");
                }
            }
            else
            {
                qc.tenqc = tenqc;
                qc.tencongty = tencongty;
                qc.hinhnen = hinhnen;
                qc.link = link;

                qc.ngaybatdau = DateTime.Parse(ngaybatdau);
                qc.ngayhethan = DateTime.Parse(ngayhethan);
                qc.trangthai = false;

                data.QuangCaos.InsertOnSubmit(qc);
                data.SubmitChanges();
            }
            
            return RedirectToAction("Index", "Home");
        }
        
        /*public ActionResult Details(int id)
        {
            var laptop = data.Laptops.Where(n => n.malaptop == id).FirstOrDefault();
            return View(laptop);
        }*/
        //dùng cho SEO
        public ActionResult Details(int id, string postName)
        {
            var laptop = data.Laptops.Where(n => n.malaptop == id).FirstOrDefault();
            return View(laptop);
        }

        public ActionResult PostDetails(int? id, string postName)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var baiviet = data.TinTucs.Where(n => n.matin == id).FirstOrDefault();
            return View(baiviet);
        }

        //xem chi tiết đơn đặt hàng
        public ActionResult InvoiceInfo(int? id)
        {
            if (id == null)
            {
                Notification.set_flash("Không tồn tại đơn hàng!", "warning");
                return RedirectToAction("Index");
            }
            DonHang donHang = data.DonHangs.Where(n => n.madon == id).FirstOrDefault();
            if (donHang == null)
            {
                Notification.set_flash("Không tồn tại  đơn hàng!", "warning");
                return RedirectToAction("Index");
            }
            ViewBag.orderDetails = data.ChiTietDonHangs.Where(m => m.madon == id).ToList();
            ViewBag.productOrder = data.Laptops.ToList();
            return View(donHang);
        }

        public ActionResult ListBaiVietTheoChuDeId(int? page, int id)
        {
            if (page == null) page = 1;
            var all_blog = (from s in data.TinTucs select s).OrderBy(m => m.matin).Where(n => n.machude == id && n.xuatban == true);
            int pageSize = 9;
            int pageNum = page ?? 1;
            return View(all_blog.ToPagedList(pageNum, pageSize));
        }

        public ActionResult GetListAllLaptop(int? page)
        {
            if (page == null) page = 1;
            var all_laptop = (from s in data.Laptops select s).OrderBy(m => m.malaptop).Where(n => n.trangthai == true);
            int pageSize = 12;
            int pageNum = page ?? 1;
            return View(all_laptop.ToPagedList(pageNum, pageSize));
        }

        public ActionResult ListLaptopTheoSearch(int? page, string SearchString)
        {
            CommonFields.seek = SearchString;
            if (page == null) page = 1;
            var all_laptop = (from s in data.Laptops select s).OrderBy(m => m.malaptop).Where(n => n.trangthai == true && (n.tenlaptop.Contains(SearchString)));
            int pageSize = 3;
            int pageNum = page ?? 1;
            return View(all_laptop.ToPagedList(pageNum, pageSize));
        }

        public ActionResult ListLaptopTheoHangId(int? page, int id)
        {
            if (page == null) page = 1;
            var all_laptop = (from s in data.Laptops select s).OrderBy(m => m.malaptop).Where(n => n.mahang == id && n.trangthai == true);
            int pageSize = 3;
            int pageNum = page ?? 1;
            return View(all_laptop.ToPagedList(pageNum, pageSize));
        }

        public ActionResult ListLaptopTheoNhuCauById(int? page, int id)
        {
            if (page == null) page = 1;
            var all_laptop = (from s in data.Laptops select s).OrderBy(m => m.malaptop).Where(n => n.manhucau == id && n.trangthai == true);
            int pageSize = 3;
            int pageNum = page ?? 1;
            return View(all_laptop.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Comment()
        {
            var comment = (from cd in data.DanhGias select cd).Where(n => n.malaptop == CommonFields.id && n.trangthai == true); ;
            return PartialView(comment);
        }
    }
}