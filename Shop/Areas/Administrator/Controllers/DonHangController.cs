using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shop.Areas.Administrator.Data.message;
using Shop.EF;

namespace Shop.Areas.Administrator.Controllers
{
    public class DonHangController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/DonHang
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                var donHangs = db.DonHangs.Include(d => d.AspNetUser);
                return View(donHangs.ToList());
            }
        }

        //xem chi tiết đơn đặt hàng
        public ActionResult InvoiceDetails(int? id)
        {
            if (id == null)
            {
                Notification.set_flash("Không tồn tại đơn hàng!", "warning");
                return RedirectToAction("Index");
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                Notification.set_flash("Không tồn tại  đơn hàng!", "warning");
                return RedirectToAction("Index");
            }
            ViewBag.orderDetails = db.ChiTietDonHangs.Where(m => m.madon == id).ToList();
            ViewBag.productOrder = db.Laptops.ToList();
            return View(donHang);
        }

        // GET: Administrator/DonHang/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (id == null)
                {
                    Notification.set_flash("Lỗi xem chi tiết đơn hàng ! (id == null)", "danger");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    Notification.set_flash("Không tìm thấy đơn hàng !", "warning");
                    return HttpNotFound();
                }
                return View(donHang);
            }
        }

        // GET: Administrator/DonHang/Create
        public ActionResult Create()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                ViewBag.makh = new SelectList(db.AspNetUsers, "Id", "Email");
                return View();
            }
        }

        // POST: Administrator/DonHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "madon,thanhtoan,giaohang,ngaydat,ngaygiao,makh,tinhtrang")] DonHang donHang)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.DonHangs.Add(donHang);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới đơn hàng thành công !", "success");
                    return RedirectToAction("Index");
                }

                ViewBag.makh = new SelectList(db.AspNetUsers, "Id", "Email", donHang.makh);
                return View(donHang);
            }
        }

        // GET: Administrator/DonHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (id == null)
                {
                    Notification.set_flash("Lỗi không tìm thấy đơn hàng cần sửa ! (id == null)", "danger");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    Notification.set_flash("Không tìm thấy đơn hàng !", "warning");
                    return HttpNotFound();
                }
                ViewBag.makh = new SelectList(db.AspNetUsers, "Id", "Email", donHang.makh);
                return View(donHang);
            }
        }

        // POST: Administrator/DonHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "madon,thanhtoan,giaohang,ngaydat,ngaygiao,makh,tinhtrang")] DonHang donHang)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(donHang).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Đã cập nhật trạng thái đơn hàng !", "success");
                    return RedirectToAction("Index");
                }
                ViewBag.makh = new SelectList(db.AspNetUsers, "Id", "Email", donHang.makh);
                return View(donHang);
            }
        }

        // GET: Administrator/DonHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (id == null)
                {
                    Notification.set_flash("Lỗi không tìm thấy đơn hàng cần xóa ! (id == null)", "danger");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    Notification.set_flash("Không tìm thấy đơn hàng !", "warning");
                    return HttpNotFound();
                }
                return View(donHang);
            }
        }

        // POST: Administrator/DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                DonHang donHang = db.DonHangs.Find(id);
                db.DonHangs.Remove(donHang);
                Notification.set_flash("Đã xóa đơn hàng thành công !", "success");
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
