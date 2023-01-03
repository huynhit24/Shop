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
    public class AspNetUsersController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Administrator/AspNetUsers
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                return View(db.AspNetUsers.ToList());
            }
        }

        // GET: Administrator/AspNetUsers/Details/5
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
                    Notification.set_flash("Không tìm thấy tài khoản !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetUser aspNetUser = db.AspNetUsers.Find(id);
                if (aspNetUser == null)
                {
                    Notification.set_flash("Không tìm thấy tài khoản !", "warning");
                    return HttpNotFound();
                }
                return View(aspNetUser);
            }
        }

        // GET: Administrator/AspNetUsers/Create
        public ActionResult Create()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                return View();
            }
        }

        // POST: Administrator/AspNetUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,ngaysinh,profile,avatar,hoten,diachi")] AspNetUser aspNetUser)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (aspNetUser.Email == null || aspNetUser.Email.Equals(""))
                    {
                        Notification.set_flash("Vui lòng nhập Email!", "danger");
                        return RedirectToAction("Index");
                    }
                    db.AspNetUsers.Add(aspNetUser);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới tài khoản đăng nhập thành công !", "success");
                    return RedirectToAction("Index");
                }

                return View(aspNetUser);
            }
        }

        // GET: Administrator/AspNetUsers/Edit/5
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
                    Notification.set_flash("Không tìm thấy tài khoản !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetUser aspNetUser = db.AspNetUsers.Find(id);
                if (aspNetUser == null)
                {
                    Notification.set_flash("Không tìm thấy tài khoản !", "warning");
                    return HttpNotFound();
                }
                return View(aspNetUser);
            }
        }

        // POST: Administrator/AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,ngaysinh,profile,avatar,hoten,diachi")] AspNetUser aspNetUser)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (aspNetUser.Email == null || aspNetUser.Email.Equals(""))
                    {
                        Notification.set_flash("Vui lòng nhập Email!", "danger");
                        return RedirectToAction("Index");
                    }
                    db.Entry(aspNetUser).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Cập nhật tài khoản thành công !", "success");
                    return RedirectToAction("Index");
                }
                return View(aspNetUser);
            }
        }

        // GET: Administrator/AspNetUsers/Delete/5
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
                    Notification.set_flash("Không tìm thấy tài khoản !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetUser aspNetUser = db.AspNetUsers.Find(id);
                if (aspNetUser == null)
                {
                    Notification.set_flash("Không tìm thấy tài khoản !", "warning");
                    return HttpNotFound();
                }
                return View(aspNetUser);
            }
        }

        // POST: Administrator/AspNetUsers/Delete/5
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
                AspNetUser aspNetUser = db.AspNetUsers.Find(id);
                db.AspNetUsers.Remove(aspNetUser);
                db.SaveChanges();
                Notification.set_flash("Xóa tài khoản Account thành công !", "success");
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

        //cập nhật hủy đơn && khôi phục đơn

        //Hủy đơn hàng
        public ActionResult DelTrashAcc(string id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (id == null)
                {
                    Notification.set_flash("Lỗi không tìm thấy tài khoản cần xóa ! (id == null)", "danger");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetUser acc = db.AspNetUsers.Find(id);
                if (acc == null)
                {
                    Notification.set_flash("Không tìm thấy tài khoản !", "warning");
                    return HttpNotFound();
                }
                acc.LockoutEnabled = false;
                db.SaveChanges();
                Notification.set_flash("Đã khóa tài khoản thành công!", "success");
                return RedirectToAction("Index");
            }
        }

        //Hủy đơn hàng
        public ActionResult UndoTrashAcc(string id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (id == null)
                {
                    Notification.set_flash("Lỗi không tìm thấy tài khoản cần xóa ! (id == null)", "danger");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetUser acc = db.AspNetUsers.Find(id);
                if (acc == null)
                {
                    Notification.set_flash("Không tìm thấy tài khoản !", "warning");
                    return HttpNotFound();
                }
                acc.LockoutEnabled = true;
                db.SaveChanges();
                Notification.set_flash("Mở khóa thành công tài khoản!", "warning");
                return RedirectToAction("Index");
            }
        }
    }
}
