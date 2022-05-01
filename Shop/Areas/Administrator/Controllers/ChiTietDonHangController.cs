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
    public class ChiTietDonHangController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/ChiTietDonHang
        public ActionResult Index()
        {
            var chiTietDonHangs = db.ChiTietDonHangs.Include(c => c.DonHang).Include(c => c.Laptop);
            return View(chiTietDonHangs.ToList());
        }

        // GET: Administrator/ChiTietDonHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        // GET: Administrator/ChiTietDonHang/Create
        public ActionResult Create()
        {
            ViewBag.madon = new SelectList(db.DonHangs, "madon", "makh");
            ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop");
            return View();
        }

        // POST: Administrator/ChiTietDonHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "madon,malaptop,soluong,dongia")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDonHangs.Add(chiTietDonHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.madon = new SelectList(db.DonHangs, "madon", "makh", chiTietDonHang.madon);
            ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop", chiTietDonHang.malaptop);
            return View(chiTietDonHang);
        }

        // GET: Administrator/ChiTietDonHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.madon = new SelectList(db.DonHangs, "madon", "makh", chiTietDonHang.madon);
            ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop", chiTietDonHang.malaptop);
            return View(chiTietDonHang);
        }

        // POST: Administrator/ChiTietDonHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "madon,malaptop,soluong,dongia")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDonHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.madon = new SelectList(db.DonHangs, "madon", "makh", chiTietDonHang.madon);
            ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop", chiTietDonHang.malaptop);
            return View(chiTietDonHang);
        }

        // GET: Administrator/ChiTietDonHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        // POST: Administrator/ChiTietDonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            db.ChiTietDonHangs.Remove(chiTietDonHang);
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
