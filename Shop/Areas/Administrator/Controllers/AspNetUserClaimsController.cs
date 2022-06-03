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
    public class AspNetUserClaimsController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/AspNetUserClaims
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                var aspNetUserClaims = db.AspNetUserClaims.Include(a => a.AspNetUser);
                return View(aspNetUserClaims.ToList());
            }
        }

        // GET: Administrator/AspNetUserClaims/Details/5
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
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetUserClaim aspNetUserClaim = db.AspNetUserClaims.Find(id);
                if (aspNetUserClaim == null)
                {
                    return HttpNotFound();
                }
                return View(aspNetUserClaim);
            }
        }

        // GET: Administrator/AspNetUserClaims/Create
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

        // POST: Administrator/AspNetUserClaims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ClaimType,ClaimValue")] AspNetUserClaim aspNetUserClaim)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.AspNetUserClaims.Add(aspNetUserClaim);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserClaim.UserId);
                return View(aspNetUserClaim);
            }
        }

        // GET: Administrator/AspNetUserClaims/Edit/5
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
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetUserClaim aspNetUserClaim = db.AspNetUserClaims.Find(id);
                if (aspNetUserClaim == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserClaim.UserId);
                return View(aspNetUserClaim);
            }
        }

        // POST: Administrator/AspNetUserClaims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ClaimType,ClaimValue")] AspNetUserClaim aspNetUserClaim)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(aspNetUserClaim).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserClaim.UserId);
                return View(aspNetUserClaim);
            }
        }

        // GET: Administrator/AspNetUserClaims/Delete/5
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
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetUserClaim aspNetUserClaim = db.AspNetUserClaims.Find(id);
                if (aspNetUserClaim == null)
                {
                    return HttpNotFound();
                }
                return View(aspNetUserClaim);
            }
        }

        // POST: Administrator/AspNetUserClaims/Delete/5
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
                AspNetUserClaim aspNetUserClaim = db.AspNetUserClaims.Find(id);
                db.AspNetUserClaims.Remove(aspNetUserClaim);
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
