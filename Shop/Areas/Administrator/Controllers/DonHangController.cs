using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using PagedList;
using Shop.Areas.Administrator.Data.message;
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

        //xuất danh sách đơn hàng đã hủy
        public ActionResult IndexCancel()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                var donHangs = db.DonHangs.Include(d => d.AspNetUser).Where(n => n.tinhtrang == "1");
                return View(donHangs.ToList());
            }
        }

        //xem chi tiết đơn đặt hàng
        public ActionResult InvoiceDetails(int? id)
        {
            if (id == null)
            {
                Notification.set_flash("Không tồn tại đơn hàng!", "warning");
                return RedirectToAction("Index");
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                Notification.set_flash("Không tồn tại  đơn hàng!", "warning");
                return RedirectToAction("Index");
            }
            ViewBag.orderDetails = db.ChiTietDonHangs.Where(m => m.madon == id).ToList();
            ViewBag.productOrder = db.Laptops.ToList();
            return View(donHang);
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
                    Notification.set_flash("Lỗi xem chi tiết đơn hàng ! (id == null)", "danger");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    Notification.set_flash("Không tìm thấy đơn hàng !", "warning");
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
                    if (donHang.ngaydat == null)
                    {
                        Notification.set_flash("Vui lòng thêm ngày đặt!", "danger");
                        return RedirectToAction("Index");
                    }
                    if (donHang.ngaygiao == null)
                    {
                        Notification.set_flash("Vui lòng thêm ngày giao!", "danger");
                        return RedirectToAction("Index");
                    }
                    if (donHang.ngaydat > donHang.ngaygiao)
                    {
                        Notification.set_flash("Ngày giao phải sau ngày đặt hàng!", "danger");
                        return RedirectToAction("Index");
                    }
                    db.DonHangs.Add(donHang);
                    db.SaveChanges();
                    Notification.set_flash("Thêm mới đơn hàng thành công !", "success");
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
                    Notification.set_flash("Lỗi không tìm thấy đơn hàng cần sửa ! (id == null)", "danger");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    Notification.set_flash("Không tìm thấy đơn hàng !", "warning");
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
                    if (donHang.ngaydat > donHang.ngaygiao)
                    {
                        Notification.set_flash("Ngày giao phải sau ngày đặt hàng!", "danger");
                        return RedirectToAction("Index");
                    }
                    if (donHang.ngaydat == null)
                    {
                        Notification.set_flash("Vui lòng thêm ngày đặt!", "danger");
                        return RedirectToAction("Index");
                    }
                    if (donHang.ngaygiao == null)
                    {
                        Notification.set_flash("Vui lòng thêm ngày giao!", "danger");
                        return RedirectToAction("Index");
                    }
                    db.Entry(donHang).State = EntityState.Modified;
                    db.SaveChanges();
                    Notification.set_flash("Đã cập nhật trạng thái đơn hàng !", "success");
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
                    Notification.set_flash("Lỗi không tìm thấy đơn hàng cần xóa ! (id == null)", "danger");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    Notification.set_flash("Không tìm thấy đơn hàng !", "warning");
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
                Notification.set_flash("Đã xóa đơn hàng thành công !", "success");
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

        //cập nhật hủy đơn && khôi phục đơn

        //Hủy đơn hàng
        public ActionResult DelTrash(int? id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (id == null)
                {
                    Notification.set_flash("Lỗi không tìm thấy đơn hàng cần xóa ! (id == null)", "danger");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    Notification.set_flash("Không tìm thấy đơn hàng !", "warning");
                    return HttpNotFound();
                }
                donHang.tinhtrang = "1";
                /*try //cập nhật lại số lượng tồn khi đơn hàng đã hủy
                {
                    Laptop lap = new Laptop(); // tạo biến Laptop lưu tạm thời
                    List<Laptop> listLap = new List<Laptop>(); //lấy ra danh sách Laptop cần cập nhật lại số lượng tồn
                    ChiTietDonHang ct = new ChiTietDonHang(); //lấy ra chi tiết đơn hàng
                    List<ChiTietDonHang> listCT = new List<ChiTietDonHang>(); // lấy ra danh sách chi tiết cửa đơn hàng có mã đơn bằng id
                    listCT = db.ChiTietDonHangs.Where(n => n.madon == id).ToList();
                    if(listCT != null)
                    {
                        foreach (var itemCT in listCT)
                        {
                            foreach (var itemLap in listLap)
                            {
                                if (itemCT.malaptop == itemLap.malaptop)
                                {
                                    lap = itemLap;
                                    lap.soluongton = lap.soluongton + itemCT.soluong;
                                    db.SaveChanges();
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Notification.set_flash("Lỗi cập nhật Số lượng tồn!", "danger");
                    return RedirectToAction("Index");
                }*/
                db.SaveChanges();
                Notification.set_flash("Đã hủy thành công đơn hàng!", "success");
                return RedirectToAction("Index");
            }  
        }

        //Hủy đơn hàng
        public ActionResult UndoTrash(int? id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (id == null)
                {
                    Notification.set_flash("Lỗi không tìm thấy đơn hàng cần xóa ! (id == null)", "danger");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    Notification.set_flash("Không tìm thấy đơn hàng !", "warning");
                    return HttpNotFound();
                }
                donHang.tinhtrang = "0";
                /*try //cập nhật lại số lượng tồn khi đơn hàng đã khôi phục
                {
                    Laptop lap = new Laptop(); // tạo biến Laptop luu tạm thời
                    List<Laptop> listLap = new List<Laptop>(); //lấy ra danh sách Laptop cần cập nhật lại số lượng tồn
                    ChiTietDonHang ct = new ChiTietDonHang(); //lấy ra chi tiết đơn hàng
                    List<ChiTietDonHang> listCT = new List<ChiTietDonHang>(); // lấy ra danh sách chi tiết cửa đơn hàng có mã đơn bằng id
                    listCT = db.ChiTietDonHangs.Where(n => n.madon == donHang.madon).ToList();
                    if (listCT != null)
                    {
                        foreach (var itemCT in listCT)
                        {
                            foreach (var itemLap in listLap)
                            {
                                if (itemCT.malaptop == itemLap.malaptop)
                                {
                                    lap = itemLap;
                                    lap.soluongton = lap.soluongton - itemCT.soluong;
                                    db.SaveChanges();
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Notification.set_flash("Lỗi cập nhật Số lượng tồn!", "danger");
                    return RedirectToAction("Index");
                }*/
                db.SaveChanges();
                Notification.set_flash("Khôi phục thành công đơn hàng!", "success");
                return RedirectToAction("Index");
            }
        }

        public ActionResult UpdateNgayGiao(int? id)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                if (id == null)
                {
                    Notification.set_flash("Lỗi không tìm thấy đơn hàng cần xóa ! (id == null)", "danger");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DonHang donHang = db.DonHangs.Find(id);
                if (donHang == null)
                {
                    Notification.set_flash("Không tìm thấy đơn hàng !", "warning");
                    return HttpNotFound();
                }
                donHang.ngaygiao = DateTime.Now;
                if (donHang.ngaydat > donHang.ngaygiao)
                {
                    Notification.set_flash("Ngày giao phải sau ngày đặt hàng!", "danger");
                    return RedirectToAction("Index");
                }
                db.SaveChanges();
                Notification.set_flash("Cập nhật ngày giao thành công!", "success");
                return RedirectToAction("Index");
            }
        }

        //tính doanh thu
        public ActionResult DoanhThu()
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                List<ChiTietDonHang> listCTDonHang = db.ChiTietDonHangs.ToList();
                //Tính doanh thu tháng hiện tại
                double DTThang = 0;
                foreach (var item in listCTDonHang)
                {
                    if (item.DonHang.ngaydat.GetValueOrDefault().Month == DateTime.Now.Month && item.DonHang.ngaydat.GetValueOrDefault().Year == DateTime.Now.Year)
                    {
                        DTThang += (double) (item.dongia * item.soluong);
                    }
                }
                double DTNgay = 0;
                foreach (var item in listCTDonHang)
                {
                    if (item.DonHang.ngaydat.GetValueOrDefault().Day == DateTime.Now.Day && item.DonHang.ngaydat.GetValueOrDefault().Month == DateTime.Now.Month && item.DonHang.ngaydat.GetValueOrDefault().Year == DateTime.Now.Year)
                    {
                        DTNgay += (double)(item.dongia * item.soluong);
                    }
                }
                double DTNam = 0;
                foreach (var item in listCTDonHang)
                {
                    if (item.DonHang.ngaydat.GetValueOrDefault().Year == DateTime.Now.Year)
                    {
                        DTNam += (double)(item.dongia * item.soluong);
                    }
                }
                if(DTThang != 0)
                {
                    ViewBag.DTThang = DTThang;
                }
                else
                {
                    ViewBag.DTThang = 0;
                }
                if (DTNam != 0)
                {
                    ViewBag.DTNgay = DTNgay;
                }
                else
                {
                    ViewBag.DTNgay = 0;
                }
                if (DTNam != 0)
                {
                    ViewBag.DTNam = DTNam;
                }
                else
                {
                    ViewBag.DTNam = 0;
                }
                return View();
            }
        }

        public FileResult Export()
        {
            DataTable dt = new DataTable("Grib");
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("Ngày"),
                new DataColumn("Tổng Doanh Thu")
            });
            var emps = db.ChiTietDonHangs.GroupBy(p => p.DonHang.ngaydat).Distinct().Select(g => new
            {
                Pla = g.Key,
                Total = g.Sum(t => t.dongia * t.soluong)
            });
            foreach (var emp in emps)
            {
                dt.Rows.Add(emp.Pla, emp.Total);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Doanh-Thu.xlsx");
                }
            }
        }

        //Lấy Data để thống kê biểu đồ tiền đơn đặt hàng
        public ActionResult GetData()
        {
            var query = db.ChiTietDonHangs.Include("DonHang")
                   .GroupBy(p => p.DonHang.madon)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.soluong * w.dongia) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}
