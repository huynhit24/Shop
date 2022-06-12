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
    public class ChuDeController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/ChuDe
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                return View(db.ChuDes.ToList());
            }
        }

        // GET: Administrator/ChuDe/Details/5
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
                    Notification.set_flash("Không tìm thấy Chủ đề !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ChuDe chuDe = db.ChuDes.Find(id);
                if (chuDe == null)
                {
                    Notification.set_flash("Không tìm thấy Chủ đề !", "warning");
                    return HttpNotFound();
                }
                return View(chuDe);
            }
        }

        // GET: Administrator/ChuDe/Create
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

        // POST: Administrator/ChuDe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //Raw: [Bind(Include = "machude,tenchude,slug,hinh")] ChuDe chuDe
        public ActionResult Create([Bind(Include = "machude,tenchude,slug")] ChuDe chuDe)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.ChuDes.Add(chuDe);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới Chủ đề blog thành công !", "success");
                    return RedirectToAction("Index");
                }

                return View(chuDe);
            }
        }

        // GET: Administrator/ChuDe/Edit/5
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
                    Notification.set_flash("Không tìm thấy Chủ đề !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ChuDe chuDe = db.ChuDes.Find(id);
                if (chuDe == null)
                {
                    Notification.set_flash("Không tìm thấy Chủ đề !", "warning");
                    return HttpNotFound();
                }
                return View(chuDe);
            }
        }

        // POST: Administrator/ChuDe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "machude,tenchude,slug")] ChuDe chuDe)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(chuDe).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Cập nhật Chủ đề blog thành công !", "success");
                    return RedirectToAction("Index");
                }
                return View(chuDe);
            }
        }

        // GET: Administrator/ChuDe/Delete/5
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
                    Notification.set_flash("Không tìm thấy Chủ đề !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ChuDe chuDe = db.ChuDes.Find(id);
                if (chuDe == null)
                {
                    Notification.set_flash("Không tìm thấy Chủ đề !", "warning");
                    return HttpNotFound();
                }
                return View(chuDe);
            }
        }

        // POST: Administrator/ChuDe/Delete/5
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
                ChuDe chuDe = db.ChuDes.Find(id);
                db.ChuDes.Remove(chuDe);
                db.SaveChanges();
                Notification.set_flash("Xóa Chủ đề blog thành công !", "success");
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
