using Shop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shop.Areas.Administrator.Repositories
{
    internal interface IDanhGiaController
    {
        ActionResult Index();
        ActionResult Details(int? id);
        ActionResult Create();
        ActionResult Create([Bind(Include = "madanhgia,ten,noidung,vote,ngaydanhgia,malaptop,trangthai")] DanhGia danhGia);
        ActionResult Edit(int? id);
        ActionResult Edit([Bind(Include = "madanhgia,ten,noidung,vote,ngaydanhgia,malaptop,trangthai")] DanhGia danhGia);
        ActionResult Delete(int? id);
        ActionResult DeleteConfirmed(int id);
    }
}
