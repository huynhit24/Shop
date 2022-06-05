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
    public class LaptopController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/Laptop
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                var laptops = db.Laptops.Include(l => l.Hang).Include(l => l.NhuCau);
                return View(laptops.ToList());
            }
        }

        // GET: Administrator/Laptop/Details/5
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
                Laptop laptop = db.Laptops.Find(id);
                if (laptop == null)
                {
                    return HttpNotFound();
                }
                return View(laptop);
            }
        }

        // GET: Administrator/Laptop/Create
        public ActionResult Create()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                ViewBag.mahang = new SelectList(db.Hangs, "mahang", "tenhang");
                ViewBag.manhucau = new SelectList(db.NhuCaus, "manhucau", "tennhucau");
                return View();
            }
        }

        // POST: Administrator/Laptop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "malaptop,tenlaptop,giaban,mota,hinh,mahang,manhucau,cpu,gpu,ram,hardware,manhinh,ngaycapnhat,soluongton,pin,trangthai")] Laptop laptop)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Laptops.Add(laptop);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới Laptop thành công !", "success");
                    return RedirectToAction("Index");
                }

                ViewBag.mahang = new SelectList(db.Hangs, "mahang", "tenhang", laptop.mahang);
                ViewBag.manhucau = new SelectList(db.NhuCaus, "manhucau", "tennhucau", laptop.manhucau);
                return View(laptop);
            }
        }

        // GET: Administrator/Laptop/Edit/5
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
                Laptop laptop = db.Laptops.Find(id);
                if (laptop == null)
                {
                    return HttpNotFound();
                }
                ViewBag.mahang = new SelectList(db.Hangs, "mahang", "tenhang", laptop.mahang);
                ViewBag.manhucau = new SelectList(db.NhuCaus, "manhucau", "tennhucau", laptop.manhucau);
                return View(laptop);
            }
        }

        // POST: Administrator/Laptop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "malaptop,tenlaptop,giaban,mota,hinh,mahang,manhucau,cpu,gpu,ram,hardware,manhinh,ngaycapnhat,soluongton,pin,trangthai")] Laptop laptop)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(laptop).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Cập nhật Laptop thành công !", "success");
                    return RedirectToAction("Index");
                }
                ViewBag.mahang = new SelectList(db.Hangs, "mahang", "tenhang", laptop.mahang);
                ViewBag.manhucau = new SelectList(db.NhuCaus, "manhucau", "tennhucau", laptop.manhucau);
                return View(laptop);
            }
        }

        // GET: Administrator/Laptop/Delete/5
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
                Laptop laptop = db.Laptops.Find(id);
                if (laptop == null)
                {
                    return HttpNotFound();
                }
                return View(laptop);
            }
        }

        // POST: Administrator/Laptop/Delete/5
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
                Laptop laptop = db.Laptops.Find(id);
                db.Laptops.Remove(laptop);
                db.SaveChanges();
                Notification.set_flash("Xóa Laptop thành công !", "success");
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

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return file.FileName;
        }
    }
}
