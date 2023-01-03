using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using Shop.Areas.Administrator.Data.excel;
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
                    Notification.set_flash("Không tìm thấy Laptop !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Laptop laptop = db.Laptops.Find(id);
                if (laptop == null)
                {
                    Notification.set_flash("Không tìm thấy Laptop !", "warning");
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
                    if (laptop.tenlaptop == null || laptop.tenlaptop.Equals(""))
                    {
                        Notification.set_flash("Vui lòng nhập tên Laptop!", "danger");
                        return RedirectToAction("Index");
                    }
                    if (laptop.giaban == null)
                    {
                        Notification.set_flash("Vui lòng nhập giá bán!", "danger");
                        return RedirectToAction("Index");
                    }
                    if (laptop.giaban <= 0)
                    {
                        Notification.set_flash("Giá bán Laptop phải > 0đ!", "danger");
                        return RedirectToAction("Index");
                    }
                    if (laptop.soluongton < 0)
                    {
                        Notification.set_flash("Số lượng tồn phải >= 0!", "danger");
                        return RedirectToAction("Index");
                    }
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
                    Notification.set_flash("Không tìm thấy Laptop !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Laptop laptop = db.Laptops.Find(id);
                if (laptop == null)
                {
                    Notification.set_flash("Không tìm thấy Laptop !", "warning");
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
                    if (laptop.tenlaptop == null || laptop.tenlaptop.Equals(""))
                    {
                        Notification.set_flash("Vui lòng nhập tên Laptop!", "danger");
                        return RedirectToAction("Index");
                    }
                    if (laptop.giaban == null)
                    {
                        Notification.set_flash("Vui lòng nhập giá bán!", "danger");
                        return RedirectToAction("Index");
                    }
                    if (laptop.giaban <= 0)
                    {
                        Notification.set_flash("Giá bán Laptop phải > 0đ!", "danger");
                        return RedirectToAction("Index");
                    }
                    if (laptop.soluongton < 0)
                    {
                        Notification.set_flash("Số lượng tồn phải >= 0!", "danger");
                        return RedirectToAction("Index");
                    }
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
                    Notification.set_flash("Không tìm thấy Laptop !", "warning");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Laptop laptop = db.Laptops.Find(id);
                if (laptop == null)
                {
                    Notification.set_flash("Không tìm thấy Laptop !", "warning");
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

        //EPPlus Excel
        public ActionResult GetLaptopsFromExcel(HttpPostedFileBase fileExcel)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                List<Laptop> list = new List<Laptop>();
                if (fileExcel != null)
                {
                    try
                    {
                        using (ExcelPackage package = new ExcelPackage(fileExcel.InputStream))
                        {
                            ExcelWorkbook workbook = package.Workbook;
                            if (workbook != null)
                            {
                                ExcelWorksheet worksheet = workbook.Worksheets.FirstOrDefault();
                                if (worksheet != null)
                                {
                                    list = worksheet.ReadExcelToList<Laptop>();
                                    foreach (var item in list)
                                    {
                                        ViewBag.listLaptop = item.tenlaptop;
                                    }
                                    //ViewBag.listLaptop = list;
                                    return RedirectToAction("ExcelDonHangImport","Laptop");
                                    //Your code
                                }
                            }
                        }

                    }
                    catch (Exception)
                    {
                        //Save error log
                    }
                }
                return RedirectToAction("Index");
            }
        }


        public ActionResult ExcelDonHangImport()
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

        //Làm nhập file Excel
        /// <summary>
        /// giải thích
        /// Sử dụng: SqlClient + OleDb
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImportDonHang(HttpPostedFileBase postedFile)
        {
            if (Session["taikhoanadmin"] == null)
            {
                return RedirectToAction("Error401", "MainPage");
            }
            else
            {
                try
                {
                    string filePath = string.Empty;
                    if (postedFile != null)
                    {
                        string path = Server.MapPath("~/Upload/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                                                
                        filePath = path + Path.GetFileName(postedFile.FileName);
                        string extension = Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(filePath);

                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls":
                                conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                                break;
                            case ".xlsx":
                                conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                                break;
                        }

                        DataTable dtExcel = new DataTable();
                        conString = string.Format(conString, filePath);
                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();

                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT *from [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dtExcel);
                                    connExcel.Close();
                                }
                            }
                        }
                        conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(conString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                sqlBulkCopy.DestinationTableName = "[dbo].[Laptop]";
                                sqlBulkCopy.ColumnMappings.Add("malaptop", "malaptop");
                                sqlBulkCopy.ColumnMappings.Add("tenlaptop", "tenlaptop");
                                sqlBulkCopy.ColumnMappings.Add("giaban", "giaban");
                                sqlBulkCopy.ColumnMappings.Add("mota", "mota");
                                sqlBulkCopy.ColumnMappings.Add("hinh", "hinh");
                                sqlBulkCopy.ColumnMappings.Add("mahang", "mahang");
                                sqlBulkCopy.ColumnMappings.Add("manhucau", "manhucau");
                                sqlBulkCopy.ColumnMappings.Add("cpu", "cpu");
                                sqlBulkCopy.ColumnMappings.Add("gpu", "gpu");
                                sqlBulkCopy.ColumnMappings.Add("ram", "ram");
                                sqlBulkCopy.ColumnMappings.Add("hardware", "hardware");
                                sqlBulkCopy.ColumnMappings.Add("manhinh", "manhinh");
                                sqlBulkCopy.ColumnMappings.Add("ngaycapnhat", "ngaycapnhat");
                                sqlBulkCopy.ColumnMappings.Add("soluongton", "soluongton");
                                sqlBulkCopy.ColumnMappings.Add("pin", "pin");
                                sqlBulkCopy.ColumnMappings.Add("trangthai", "trangthai");
                                con.Open();
                                sqlBulkCopy.WriteToServer(dtExcel);
                                con.Close();
                            }
                        }

                    }
                    Notification.set_flash("Nhập Excel Laptop thành công!", "success");
                    return RedirectToAction("Index","Laptop");
                }
                catch (Exception)
                {
                    Notification.set_flash("Lỗi nhập File Excel!", "danger");
                    return RedirectToAction("Index", "Laptop");
                }
            }
        }


    }
}
