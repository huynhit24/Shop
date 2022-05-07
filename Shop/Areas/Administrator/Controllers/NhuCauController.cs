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
    public class NhuCauController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/NhuCau
        public ActionResult Index()
        {
            return View(db.NhuCaus.ToList());
        }

        // GET: Administrator/NhuCau/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhuCau nhuCau = db.NhuCaus.Find(id);
            if (nhuCau == null)
            {
                return HttpNotFound();
            }
            return View(nhuCau);
        }

        // GET: Administrator/NhuCau/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/NhuCau/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "manhucau,tennhucau")] NhuCau nhuCau)
        {
            if (ModelState.IsValid)
            {
                db.NhuCaus.Add(nhuCau);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhuCau);
        }

        // GET: Administrator/NhuCau/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhuCau nhuCau = db.NhuCaus.Find(id);
            if (nhuCau == null)
            {
                return HttpNotFound();
            }
            return View(nhuCau);
        }

        // POST: Administrator/NhuCau/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "manhucau,tennhucau")] NhuCau nhuCau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhuCau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhuCau);
        }

        // GET: Administrator/NhuCau/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhuCau nhuCau = db.NhuCaus.Find(id);
            if (nhuCau == null)
            {
                return HttpNotFound();
            }
            return View(nhuCau);
        }

        // POST: Administrator/NhuCau/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhuCau nhuCau = db.NhuCaus.Find(id);
            db.NhuCaus.Remove(nhuCau);
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
