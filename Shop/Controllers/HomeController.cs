using PagedList;
using Shop.Common;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult DonHangDaMua()
        {
            if (Session["TaiKhoan"] != null)
            {
                AspNetUser kh = (AspNetUser)Session["TaiKhoan"];
                /*List<DonHang> list = kh.DonHangs.ToList();*/
                List<DonHang> list = data.DonHangs.Where(n => n.makh == kh.Id).ToList();
                return View(list);
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
        public ActionResult Contact(FormCollection collection, LienHe lh)
        {
            var hoten = collection["hoten"];
            var email = collection["email"];
            var dienthoai = collection["dienthoai"];
            var website = collection["website"];
            var noidung = collection["noidung"];
            /*var trangthai = collection["trangthai"];*/

            lh.hoten = hoten;
            lh.email = email;
            lh.dienthoai = dienthoai;
            lh.website = website;
            lh.noidung = noidung;
            lh.trangthai = true;

            data.LienHes.InsertOnSubmit(lh);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult NhanXet()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult NhanXet(FormCollection collection, DanhGia dg)
        {
            var ten = collection["ten"];
            var noidung = collection["noidung"];
            var vote = collection["vote"];
            var malaptop = CommonFields.id;
            /*var trangthai = collection["trangthai"];*/
            dg.ten = ten;
            dg.noidung = noidung;
            /*dg.vote = Convert.ToInt32(vote);*/
            dg.vote = Convert.ToInt32(vote);
            dg.ngaydanhgia = DateTime.Now;
            dg.malaptop = malaptop;

            data.DanhGias.InsertOnSubmit(dg);
            data.SubmitChanges();
            /*return RedirectToAction("Details");*/
            return PartialView();
        }

        [HttpGet]
        public ActionResult QuangCao()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuangCao(FormCollection collection, QuangCao qc)
        {
            var tenqc = collection["tenqc"];
            var tencongty = collection["tencongty"];
            var hinhnen = collection["hinhnen"];
            var link = collection["link"];
            var ngaybatdau = String.Format("{0:MM/dd/yyyy}", collection["ngaybatdau"]);
            var ngayhethan = String.Format("{0:MM/dd/yyyy}", collection["ngayhethan"]);

            qc.tenqc = tenqc;
            qc.tencongty = tencongty;
            qc.hinhnen = hinhnen;
            qc.link = link;

            qc.ngaybatdau = DateTime.Parse(ngaybatdau);
            qc.ngayhethan = DateTime.Parse(ngayhethan);
            qc.trangthai = false;

            data.QuangCaos.InsertOnSubmit(qc);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var laptop = data.Laptops.Where(n => n.malaptop == id).FirstOrDefault();
            return View(laptop);
        }

        public ActionResult PostDetails(int id)
        {
            MyDataDataContext data = new MyDataDataContext();
            var baiviet = data.TinTucs.Where(n => n.matin == id).FirstOrDefault();
            return View(baiviet);
        }

        public ActionResult ListBaiVietTheoChuDeId(int? page, int id)
        {
            if (page == null) page = 1;
            var all_blog = (from s in data.TinTucs select s).OrderBy(m => m.matin).Where(n => n.machude == id && n.xuatban == true);
            int pageSize = 9;
            int pageNum = page ?? 1;
            return View(all_blog.ToPagedList(pageNum, pageSize));
        }

        public ActionResult ListLaptopTheoSearch(int? page, string SearchString)
        {
            CommonFields.seek = SearchString;
            if (page == null) page = 1;
            var all_laptop = (from s in data.Laptops select s).OrderBy(m => m.malaptop).Where(n => n.trangthai == true && n.tenlaptop.Contains(SearchString));
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
            var comment = (from cd in data.DanhGias select cd).Where(n => n.malaptop == CommonFields.id); ;
            return PartialView(comment);
        }
    }
}