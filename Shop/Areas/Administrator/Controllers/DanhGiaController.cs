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
    public class DanhGiaController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/DanhGia
        public ActionResult Index()
        {
            var danhGias = db.DanhGias.Include(d => d.Laptop);
            return View(danhGias.ToList());
        }

        // GET: Administrator/DanhGia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGia danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return HttpNotFound();
            }
            return View(danhGia);
        }

        // GET: Administrator/DanhGia/Create
        public ActionResult Create()
        {
            ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop");
            return View();
        }

        // POST: Administrator/DanhGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "madanhgia,ten,noidung,vote,ngaydanhgia,malaptop,trangthai")] DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                db.DanhGias.Add(danhGia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop", danhGia.malaptop);
            return View(danhGia);
        }

        // GET: Administrator/DanhGia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGia danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return HttpNotFound();
            }
            ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop", danhGia.malaptop);
            return View(danhGia);
        }

        // POST: Administrator/DanhGia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "madanhgia,ten,noidung,vote,ngaydanhgia,malaptop,trangthai")] DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhGia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop", danhGia.malaptop);
            return View(danhGia);
        }

        // GET: Administrator/DanhGia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGia danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return HttpNotFound();
            }
            return View(danhGia);
        }

        // POST: Administrator/DanhGia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DanhGia danhGia = db.DanhGias.Find(id);
            db.DanhGias.Remove(danhGia);
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
