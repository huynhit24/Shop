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
    public class MetaLaptopController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/MetaLaptop
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                var metaLaptops = db.MetaLaptops.Include(m => m.Laptop);
                return View(metaLaptops.ToList());
            }
        }

        // GET: Administrator/MetaLaptop/Details/5
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
                    Notification.set_flash("Không tìm thấy META-LAPTOP !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MetaLaptop metaLaptop = db.MetaLaptops.Find(id);
                if (metaLaptop == null)
                {
                    Notification.set_flash("Không tìm thấy META-LAPTOP !", "warning");
                    return HttpNotFound();
                }
                return View(metaLaptop);
            }
        }

        // GET: Administrator/MetaLaptop/Create
        public ActionResult Create()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop");
                return View();
            }
        }

        // POST: Administrator/MetaLaptop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mameta,keymeta,valuemeta,malaptop")] MetaLaptop metaLaptop)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.MetaLaptops.Add(metaLaptop);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới METALAPTOP thành công !", "success");
                    return RedirectToAction("Index");
                }

                ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop", metaLaptop.malaptop);
                return View(metaLaptop);
            }
        }

        // GET: Administrator/MetaLaptop/Edit/5
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
                    Notification.set_flash("Không tìm thấy META-LAPTOP !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MetaLaptop metaLaptop = db.MetaLaptops.Find(id);
                if (metaLaptop == null)
                {
                    Notification.set_flash("Không tìm thấy META-LAPTOP !", "warning");
                    return HttpNotFound();
                }
                ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop", metaLaptop.malaptop);
                return View(metaLaptop);
            }
        }

        // POST: Administrator/MetaLaptop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mameta,keymeta,valuemeta,malaptop")] MetaLaptop metaLaptop)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(metaLaptop).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Cập nhật METALAPTOP thành công !", "success");
                    return RedirectToAction("Index");
                }
                ViewBag.malaptop = new SelectList(db.Laptops, "malaptop", "tenlaptop", metaLaptop.malaptop);
                return View(metaLaptop);
            }
        }

        // GET: Administrator/MetaLaptop/Delete/5
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
                    Notification.set_flash("Không tìm thấy META-LAPTOP !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MetaLaptop metaLaptop = db.MetaLaptops.Find(id);
                if (metaLaptop == null)
                {
                    Notification.set_flash("Không tìm thấy META-LAPTOP !", "warning");
                    return HttpNotFound();
                }
                return View(metaLaptop);
            }
        }

        // POST: Administrator/MetaLaptop/Delete/5
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
                MetaLaptop metaLaptop = db.MetaLaptops.Find(id);
                db.MetaLaptops.Remove(metaLaptop);
                db.SaveChanges();
                Notification.set_flash("Xóa METALAPTOP thành công !", "success");
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
