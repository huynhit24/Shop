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
    public class BinhLuanController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/BinhLuan
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                var binhLuans = db.BinhLuans.Include(b => b.TinTuc);
                return View(binhLuans.ToList());
            }
        }

        // GET: Administrator/BinhLuan/Details/5
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
                    Notification.set_flash("Không tìm thấy Bình luận !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BinhLuan binhLuan = db.BinhLuans.Find(id);
                if (binhLuan == null)
                {
                    Notification.set_flash("Không tìm thấy Bình luận !", "warning");
                    return HttpNotFound();
                }
                return View(binhLuan);
            }
        }

        // GET: Administrator/BinhLuan/Create
        public ActionResult Create()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                ViewBag.matin = new SelectList(db.TinTucs, "matin", "tieude");
                return View();
            }
        }

        // POST: Administrator/BinhLuan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mabinhluan,ten,noidung,vote,ngaybinhluan,matin,trangthai")] BinhLuan binhLuan)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.BinhLuans.Add(binhLuan);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới Bình luận blog thành công !", "success");
                    return RedirectToAction("Index");
                }

                ViewBag.matin = new SelectList(db.TinTucs, "matin", "tieude", binhLuan.matin);
                return View(binhLuan);
            }
        }

        // GET: Administrator/BinhLuan/Edit/5
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
                    Notification.set_flash("Không tìm thấy Bình luận !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BinhLuan binhLuan = db.BinhLuans.Find(id);
                if (binhLuan == null)
                {
                    Notification.set_flash("Không tìm thấy Bình luận !", "warning");
                    return HttpNotFound();
                }
                ViewBag.matin = new SelectList(db.TinTucs, "matin", "tieude", binhLuan.matin);
                return View(binhLuan);
            }
        }

        // POST: Administrator/BinhLuan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mabinhluan,ten,noidung,vote,ngaybinhluan,matin,trangthai")] BinhLuan binhLuan)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(binhLuan).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Cập nhật Bình luận blog thành công !", "success");
                    return RedirectToAction("Index");
                }
                ViewBag.matin = new SelectList(db.TinTucs, "matin", "tieude", binhLuan.matin);
                return View(binhLuan);
            }
        }

        // GET: Administrator/BinhLuan/Delete/5
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
                    Notification.set_flash("Không tìm thấy Bình luận !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BinhLuan binhLuan = db.BinhLuans.Find(id);
                if (binhLuan == null)
                {
                    Notification.set_flash("Không tìm thấy Bình luận !", "warning");
                    return HttpNotFound();
                }
                return View(binhLuan);
            }
        }

        // POST: Administrator/BinhLuan/Delete/5
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
                BinhLuan binhLuan = db.BinhLuans.Find(id);
                db.BinhLuans.Remove(binhLuan);
                db.SaveChanges();
                Notification.set_flash("Xóa bình luận blog thành công !", "success");
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
