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
    public class HangController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/Hang
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                return View(db.Hangs.ToList());
            }
        }

        // GET: Administrator/Hang/Details/5
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
                    Notification.set_flash("Không tìm thấy Hãng !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Hang hang = db.Hangs.Find(id);
                if (hang == null)
                {
                    Notification.set_flash("Không tìm thấy Hãng !", "warning");
                    return HttpNotFound();
                }
                return View(hang);
            }
        }

        // GET: Administrator/Hang/Create
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

        // POST: Administrator/Hang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mahang,tenhang,hinh")] Hang hang)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Hangs.Add(hang);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới hãng thành công !", "success");
                    return RedirectToAction("Index");
                }

                return View(hang);
            }
        }

        // GET: Administrator/Hang/Edit/5
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
                    Notification.set_flash("Không tìm thấy Hãng !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Hang hang = db.Hangs.Find(id);
                if (hang == null)
                {
                    Notification.set_flash("Không tìm thấy Hãng !", "warning");
                    return HttpNotFound();
                }
                return View(hang);
            }
        }

        // POST: Administrator/Hang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mahang,tenhang,hinh")] Hang hang)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(hang).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Cập nhật hãng thành công !", "success");
                    return RedirectToAction("Index");
                }
                return View(hang);
            }
        }

        // GET: Administrator/Hang/Delete/5
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
                    Notification.set_flash("Không tìm thấy Hãng !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Hang hang = db.Hangs.Find(id);
                if (hang == null)
                {
                    Notification.set_flash("Không tìm thấy Hãng !", "warning");
                    return HttpNotFound();
                }
                return View(hang);
            }
        }

        // POST: Administrator/Hang/Delete/5
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
                Hang hang = db.Hangs.Find(id);
                db.Hangs.Remove(hang);
                db.SaveChanges();
                Notification.set_flash("Xóa hãng thành công !", "success");
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
