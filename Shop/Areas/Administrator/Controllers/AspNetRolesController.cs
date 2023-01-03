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
    public class AspNetRolesController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/AspNetRoles
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                return View(db.AspNetRoles.ToList());
            }
        }

        // GET: Administrator/AspNetRoles/Details/5
        public ActionResult Details(string id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetRole aspNetRole = db.AspNetRoles.Find(id);
                if (aspNetRole == null)
                {
                    return HttpNotFound();
                }
                return View(aspNetRole);
            }
        }

        // GET: Administrator/AspNetRoles/Create
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

        // POST: Administrator/AspNetRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AspNetRole aspNetRole)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.AspNetRoles.Add(aspNetRole);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới quyền Role thành công !", "success");
                    return RedirectToAction("Index");
                }

                return View(aspNetRole);
            }
        }

        // GET: Administrator/AspNetRoles/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetRole aspNetRole = db.AspNetRoles.Find(id);
                if (aspNetRole == null)
                {
                    return HttpNotFound();
                }
                return View(aspNetRole);
            }
        }

        // POST: Administrator/AspNetRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] AspNetRole aspNetRole)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(aspNetRole).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Cập nhật Role thành công !", "success");
                    return RedirectToAction("Index");
                }
                return View(aspNetRole);
            }
        }

        // GET: Administrator/AspNetRoles/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetRole aspNetRole = db.AspNetRoles.Find(id);
                if (aspNetRole == null)
                {
                    return HttpNotFound();
                }
                return View(aspNetRole);
            }
        }

        // POST: Administrator/AspNetRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                AspNetRole aspNetRole = db.AspNetRoles.Find(id);
                db.AspNetRoles.Remove(aspNetRole);
                db.SaveChanges();
                Notification.set_flash("Xóa Role thành công !", "success");
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
