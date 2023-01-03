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
    public class TinTucController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/TinTuc
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                var tinTucs = db.TinTucs.Include(t => t.ChuDe);
                return View(tinTucs.ToList());
            }
        }

        // GET: Administrator/TinTuc/Details/5
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
                    Notification.set_flash("Không tìm thấy Bài viết !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TinTuc tinTuc = db.TinTucs.Find(id);
                if (tinTuc == null)
                {
                    Notification.set_flash("Không tìm thấy Bài viết !", "warning");
                    return HttpNotFound();
                }
                return View(tinTuc);
            }
        }

        // GET: Administrator/TinTuc/Create
        public ActionResult Create()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                ViewBag.machude = new SelectList(db.ChuDes, "machude", "tenchude");
                return View();
            }
        }

        // POST: Administrator/TinTuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "matin,tieude,hinhnen,tomtat,slug,noidung,luotxem,ngaycapnhat,xuatban,machude")] TinTuc tinTuc)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.TinTucs.Add(tinTuc);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới bài viết thành công !", "success");
                    return RedirectToAction("Index");
                }

                ViewBag.machude = new SelectList(db.ChuDes, "machude", "tenchude", tinTuc.machude);
                return View(tinTuc);
            }
        }

        // GET: Administrator/TinTuc/Edit/5
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
                    Notification.set_flash("Không tìm thấy Bài viết !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TinTuc tinTuc = db.TinTucs.Find(id);
                if (tinTuc == null)
                {
                    Notification.set_flash("Không tìm thấy Bài viết !", "warning");
                    return HttpNotFound();
                }
                ViewBag.machude = new SelectList(db.ChuDes, "machude", "tenchude", tinTuc.machude);
                return View(tinTuc);
            }
        }

        // POST: Administrator/TinTuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "matin,tieude,hinhnen,tomtat,slug,noidung,luotxem,ngaycapnhat,xuatban,machude")] TinTuc tinTuc)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tinTuc).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Cập nhật bài viết thành công !", "success");
                    return RedirectToAction("Index");
                }
                ViewBag.machude = new SelectList(db.ChuDes, "machude", "tenchude", tinTuc.machude);
                return View(tinTuc);
            }
        }

        // GET: Administrator/TinTuc/Delete/5
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
                    Notification.set_flash("Không tìm thấy Bài viết !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TinTuc tinTuc = db.TinTucs.Find(id);
                if (tinTuc == null)
                {
                    Notification.set_flash("Không tìm thấy Bài viết !", "warning");
                    return HttpNotFound();
                }
                return View(tinTuc);
            }
        }

        // POST: Administrator/TinTuc/Delete/5
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
                TinTuc tinTuc = db.TinTucs.Find(id);
                db.TinTucs.Remove(tinTuc);
                db.SaveChanges();
                Notification.set_flash("Xóa bài viết thành công !", "success");
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
