using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shop.EF;

namespace Shop.Areas.Administrator.Controllers
{
    public class AspNetUserLoginsController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/AspNetUserLogins
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                var aspNetUserLogins = db.AspNetUserLogins.Include(a => a.AspNetUser);
                return View(aspNetUserLogins.ToList());
            }
        }

        // GET: Administrator/AspNetUserLogins/Details/5
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
                AspNetUserLogin aspNetUserLogin = db.AspNetUserLogins.Find(id);
                if (aspNetUserLogin == null)
                {
                    return HttpNotFound();
                }
                return View(aspNetUserLogin);
            }
        }

        // GET: Administrator/AspNetUserLogins/Create
        public ActionResult Create()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
                return View();
            }
        }

        // POST: Administrator/AspNetUserLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginProvider,ProviderKey,UserId")] AspNetUserLogin aspNetUserLogin)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.AspNetUserLogins.Add(aspNetUserLogin);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserLogin.UserId);
                return View(aspNetUserLogin);
            }
        }

        // GET: Administrator/AspNetUserLogins/Edit/5
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
                AspNetUserLogin aspNetUserLogin = db.AspNetUserLogins.Find(id);
                if (aspNetUserLogin == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserLogin.UserId);
                return View(aspNetUserLogin);
            }
        }

        // POST: Administrator/AspNetUserLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginProvider,ProviderKey,UserId")] AspNetUserLogin aspNetUserLogin)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(aspNetUserLogin).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserLogin.UserId);
                return View(aspNetUserLogin);
            }
        }

        // GET: Administrator/AspNetUserLogins/Delete/5
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
                AspNetUserLogin aspNetUserLogin = db.AspNetUserLogins.Find(id);
                if (aspNetUserLogin == null)
                {
                    return HttpNotFound();
                }
                return View(aspNetUserLogin);
            }
        }

        // POST: Administrator/AspNetUserLogins/Delete/5
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
                AspNetUserLogin aspNetUserLogin = db.AspNetUserLogins.Find(id);
                db.AspNetUserLogins.Remove(aspNetUserLogin);
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
