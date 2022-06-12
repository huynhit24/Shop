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
    public class LienHeController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/LienHe
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                return View(db.LienHes.ToList());
            }
        }

        // GET: Administrator/LienHe/Details/5
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
                    Notification.set_flash("Không tìm thấy Liên hệ !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LienHe lienHe = db.LienHes.Find(id);
                if (lienHe == null)
                {
                    Notification.set_flash("Không tìm thấy Liên hệ !", "warning");
                    return HttpNotFound();
                }
                return View(lienHe);
            }
        }

        // GET: Administrator/LienHe/Create
        public ActionResult Create()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                return View();
            }
        }

        // POST: Administrator/LienHe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "malienhe,hoten,email,dienthoai,website,noidung,trangthai")] LienHe lienHe)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.LienHes.Add(lienHe);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới Liên hệ (Contact) thành công !", "success");
                    return RedirectToAction("Index");
                }

                return View(lienHe);
            }
        }

        // GET: Administrator/LienHe/Edit/5
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
                    Notification.set_flash("Không tìm thấy Liên hệ !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LienHe lienHe = db.LienHes.Find(id);
                if (lienHe == null)
                {
                    Notification.set_flash("Không tìm thấy Liên hệ !", "warning");
                    return HttpNotFound();
                }
                return View(lienHe);
            }
        }

        // POST: Administrator/LienHe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "malienhe,hoten,email,dienthoai,website,noidung,trangthai")] LienHe lienHe)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(lienHe).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Cập nhật Liên hệ (Contact) thành công !", "success");
                    return RedirectToAction("Index");
                }
                return View(lienHe);
            }
        }

        // GET: Administrator/LienHe/Delete/5
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
                    Notification.set_flash("Không tìm thấy Liên hệ !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LienHe lienHe = db.LienHes.Find(id);
                if (lienHe == null)
                {
                    Notification.set_flash("Không tìm thấy Liên hệ !", "warning");
                    return HttpNotFound();
                }
                return View(lienHe);
            }
        }

        // POST: Administrator/LienHe/Delete/5
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
                LienHe lienHe = db.LienHes.Find(id);
                db.LienHes.Remove(lienHe);
                db.SaveChanges();
                Notification.set_flash("Xóa Liên hệ (Contact) thành công !", "success");
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
