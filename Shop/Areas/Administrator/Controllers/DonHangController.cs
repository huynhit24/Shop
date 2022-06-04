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
    public class DonHangController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/DonHang
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                var donHangs = db.DonHangs.Include(d => d.AspNetUser);
                return View(donHangs.ToList());
            }
        }

        // GET: Administrator/DonHang/Details/5
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
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    return HttpNotFound();
                }
                return View(donHang);
            }
        }

        // GET: Administrator/DonHang/Create
        public ActionResult Create()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                ViewBag.makh = new SelectList(db.AspNetUsers, "Id", "Email");
                return View();
            }
        }

        // POST: Administrator/DonHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "madon,thanhtoan,giaohang,ngaydat,ngaygiao,makh,tinhtrang")] DonHang donHang)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.DonHangs.Add(donHang);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.makh = new SelectList(db.AspNetUsers, "Id", "Email", donHang.makh);
                return View(donHang);
            }
        }

        // GET: Administrator/DonHang/Edit/5
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
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    return HttpNotFound();
                }
                ViewBag.makh = new SelectList(db.AspNetUsers, "Id", "Email", donHang.makh);
                return View(donHang);
            }
        }

        // POST: Administrator/DonHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "madon,thanhtoan,giaohang,ngaydat,ngaygiao,makh,tinhtrang")] DonHang donHang)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(donHang).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.makh = new SelectList(db.AspNetUsers, "Id", "Email", donHang.makh);
                return View(donHang);
            }
        }

        // GET: Administrator/DonHang/Delete/5
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
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    return HttpNotFound();
                }
                return View(donHang);
            }
        }

        // POST: Administrator/DonHang/Delete/5
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
                DonHang donHang = db.DonHangs.Find(id);
                db.DonHangs.Remove(donHang);
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
