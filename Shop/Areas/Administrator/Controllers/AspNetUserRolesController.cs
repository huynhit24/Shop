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
    public class AspNetUserRolesController : Controller
    {
        //ĐỂ LÀM PHÂN QUYỀN TA THÊM 1 TRƯỜNG ROLE KHÁC OR ĐỔI MÃ LIÊN KẾT
        private DataModel db = new DataModel();

        // GET: Administrator/AspNetUserRoles
        public ActionResult Index()
        {
            var aspNetUserRoles = db.AspNetUserRoles.Include(a => a.AspNetRole).Include(a => a.AspNetUser);
            return View(aspNetUserRoles.ToList());
        }

        // GET: Administrator/AspNetUserRoles/Details/5
        public ActionResult Details(string id)//public ActionResult Details(string id, string roleId)
        {
            if (id == null)//id == null || roleId == null
            {
                Notification.set_flash("Không tìm thấy phần quyền này !", "warning");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
            //AspNetUserRole aspNetUserRole = db.AspNetUserRoles.FirstOrDefault(n => n.UserId == id && n.RoleId == roleId);
            AspNetUserRole aspNetUserRole = db.AspNetUserRoles.FirstOrDefault(n => n.UserId == id);
            if (aspNetUserRole == null)
            {
                Notification.set_flash("Không tìm thấy phần quyền này !", "warning");
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
                Notification.set_flash("Thêm mới quyền cho tài khoản thành công!", "success");
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRole.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }

        // GET: Administrator/AspNetUserRoles/Edit/5
        public ActionResult Edit(string id)//public ActionResult Details(string id, string roleId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
            //AspNetUserRole aspNetUserRole = db.AspNetUserRoles.FirstOrDefault(n => n.UserId == id && n.RoleId == roleId);
            AspNetUserRole aspNetUserRole = db.AspNetUserRoles.FirstOrDefault(n => n.UserId == id);
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
                Notification.set_flash("Cập nhật quyền tài khoản thành công !", "success");
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRole.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }

        // GET: Administrator/AspNetUserRoles/Delete/5
        public ActionResult Delete(string id)//public ActionResult Details(string id, string roleId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
            //AspNetUserRole aspNetUserRole = db.AspNetUserRoles.FirstOrDefault(n => n.UserId == id && n.RoleId == roleId);
            AspNetUserRole aspNetUserRole = db.AspNetUserRoles.FirstOrDefault(n => n.UserId == id);
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
            Notification.set_flash("Xóa quyền tài khoản thành công !", "success");
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
