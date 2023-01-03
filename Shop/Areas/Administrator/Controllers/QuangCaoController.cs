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
    public class QuangCaoController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/QuangCao
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                return View(db.QuangCaos.ToList());
            }
        }

        // GET: Administrator/QuangCao/Details/5
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
                    Notification.set_flash("Không tìm thấy Quảng cáo !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                QuangCao quangCao = db.QuangCaos.Find(id);
                if (quangCao == null)
                {
                    Notification.set_flash("Không tìm thấy Quảng cáo !", "warning");
                    return HttpNotFound();
                }
                return View(quangCao);
            }
        }

        // GET: Administrator/QuangCao/Create
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

        // POST: Administrator/QuangCao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maqc,tenqc,tencongty,hinhnen,link,ngaybatdau,ngayhethan,trangthai")] QuangCao quangCao)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.QuangCaos.Add(quangCao);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới QUẢNG CÁO thành công !", "success");
                    return RedirectToAction("Index");
                }

                return View(quangCao);
            }
        }

        // GET: Administrator/QuangCao/Edit/5
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
                    Notification.set_flash("Không tìm thấy Quảng cáo !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                QuangCao quangCao = db.QuangCaos.Find(id);
                if (quangCao == null)
                {
                    Notification.set_flash("Không tìm thấy Quảng cáo !", "warning");
                    return HttpNotFound();
                }
                return View(quangCao);
            }
        }

        // POST: Administrator/QuangCao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maqc,tenqc,tencongty,hinhnen,link,ngaybatdau,ngayhethan,trangthai")] QuangCao quangCao)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(quangCao).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Cập nhật QUẢNG CÁO thành công !", "success");
                    return RedirectToAction("Index");
                }
                return View(quangCao);
            }
        }

        // GET: Administrator/QuangCao/Delete/5
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
                    Notification.set_flash("Không tìm thấy Quảng cáo !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                QuangCao quangCao = db.QuangCaos.Find(id);
                if (quangCao == null)
                {
                    Notification.set_flash("Không tìm thấy Quảng cáo !", "warning");
                    return HttpNotFound();
                }
                return View(quangCao);
            }
        }

        // POST: Administrator/QuangCao/Delete/5
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
                QuangCao quangCao = db.QuangCaos.Find(id);
                db.QuangCaos.Remove(quangCao);
                db.SaveChanges();
                Notification.set_flash("Xóa QUẢNG CÁO thành công !", "success");
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
