using Shop.Common;
//using Shop.EF;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shop.Controllers;
using System.Net;
using System.Threading.Tasks;
using System.Security.Claims;
using Shop.Areas.Administrator.Data.message;

namespace Shop.Areas.Administrator.Controllers
{
    public class MainPageController : AccountController
    {
        // GET: Administrator/MainPage
        //DataModel db = new DataModel();
       /* MyDataDataContext db = new MyDataDataContext();

        private ApplicationDbContext db2 = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }*/

        // GET: Admin/adm_MainPage
        public ActionResult Index() // đang dùng
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = ""});
            }
            else
            {
                ViewBag.Online = HttpContext.Application["Online"].ToString();
                ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString(); // Số lượng người truy cập website (user && admin)
                return View();
            }
        }

        public ActionResult Error401() // đang dùng
        {
            return View();
        }

        //----------------------------Login Admin-----------------------------
        /*public bool AuthAdmin()
        {
            var user = db.AspNetUsers.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user == null)
                return false;
            var userExist = user.AspNetUserRoles.FirstOrDefault(r => r.UserId == user.Id);
            if (userExist == null)
                return false;
            if (userExist.RoleId != "1")
                return false;
            return true;
        }*/

        //---------------------------------------------------------------------

        /*[HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {

            string taikhoan = collection["Email"];
            string matkhau = collection["Password"];

            if (String.IsNullOrEmpty(taikhoan))
            {
                if (String.IsNullOrEmpty(matkhau))
                {
                    ViewBag.ThongBao = "Thông tin đăng nhập đang trống";
                }
                else
                    ViewBag.ThongBao = "Vui lòng điền Account";
            }
            else
            {
                if (String.IsNullOrEmpty(matkhau))
                {
                    ViewBag.ThongBao = "Vui lòng điền Password";
                }
                else
                {
                    //var user = db.AspNetUsers.FirstOrDefault(u => u.UserName == User.Identity.Name);
                    //if (user == null)
                    //{
                    //    ViewBag.ThongBao = "Vui lòng điền Password";
                    //}
                    AspNetUser ad_email = db.AspNetUsers.SingleOrDefault(n => n.UserName == taikhoan);
                    if (ad_email == null)
                    {
                        //ViewBag.ThongBao = "Email không đúng hoặc không tồn tại hoặc mật khẩu không đúng!";
                        ViewBag.ThongBao = "Thông tin đăng nhập không đúng";
                    }
                    else
                    {
                        PasswordHasher ph = new PasswordHasher();
                        string hashpass = ph.HashPassword(matkhau);
                        PasswordVerificationResult isCorrect = ph.VerifyHashedPassword(ad_email.PasswordHash, matkhau);
                        if (isCorrect.ToString() == "Success")
                        {
                            AspNetUserRole ad_role = db.AspNetUserRoles.SingleOrDefault(n => n.UserId == ad_email.Id);
                            if (ad_role == null || ad_role.UserId != "1")
                            {
                                ViewBag.ThongBao = "Email này không nằm trong tài khoản Admin!";
                            }
                            else
                            {
                                Session["taikhoanadmin"] = ad_email;
                                return RedirectToAction("Index", "MainPage");
                            }
                        }
                        
                    } 
                }
            }
            return View();
        }*/

        #region LoginAdmin tạm thời không dùng nữa
        //
        // GET: /Account/Login
        /*[AllowAnonymous]
        public ActionResult LoginAdmin()
        {
            return View();
        }*/

        //
        // POST: /Account/Login
        /*[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAdmin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ThongBao = "Vui lòng điền đầy đủ và đúng Account!";
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    {
                        if (!AuthAdmin())
                            return RedirectToAction("Error401", "MainPage");
                        var kh = db.AspNetUsers.Where(p => p.Email == model.Email).FirstOrDefault();
                        Session["taikhoanadmin"] = kh;// gán kh vào session admin
                        return RedirectToAction("Index","MainPage");
                    }
                case SignInStatus.Failure:
                    return View("Error");
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }*/
        #endregion

        public ActionResult Logout()// không dùng đến
        {
            Session["TaiKhoan"] = null;
            Session["taikhoanadmin"] = null;
            Notification.set_flash("Đăng xuất thành công!", "success");
            return RedirectToAction("Login", "Account", new { area = "" });
        }

        //thông kê số người truy cập website
        public ActionResult BangDieuKhien()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Online = HttpContext.Application["Online"].ToString();
                ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString(); // Số lượng người truy cập website (user && admin)
                return PartialView();
            }
        }
    }
}