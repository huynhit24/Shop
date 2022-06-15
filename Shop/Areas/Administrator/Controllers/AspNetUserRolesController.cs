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
    public class AspNetUserRolesController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/AspNetUserRoles
        public ActionResult Index()
        {
            var aspNetUserRoles = db.AspNetUserRoles.Include(a => a.AspNetRole).Include(a => a.AspNetUser);
            return View(aspNetUserRoles.ToList());
        }

        // GET: Administrator/AspNetUserRoles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
            if (aspNetUserRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserRole);
        }

        // GET: Administrator/AspNetUserRoles/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Administrator/AspNetUserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,RoleId,Note")] AspNetUserRole aspNetUserRole)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUserRoles.Add(aspNetUserRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRole.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }

        // GET: Administrator/AspNetUserRoles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
            if (aspNetUserRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRole.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }

        // POST: Administrator/AspNetUserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,RoleId,Note")] AspNetUserRole aspNetUserRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUserRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRole.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }

        // GET: Administrator/AspNetUserRoles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
            if (aspNetUserRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserRole);
        }

        // POST: Administrator/AspNetUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
            db.AspNetUserRoles.Remove(aspNetUserRole);
            db.SaveChanges();
            return RedirectToAction("Index");
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
