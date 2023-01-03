using Shop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shop.Areas.Administrator.Repositories
{
    internal interface IBinhLuanController
    {
        ActionResult Index();
        ActionResult Details(int? id);
        ActionResult Create();
        ActionResult Create([Bind(Include = "mabinhluan,ten,noidung,vote,ngaybinhluan,matin,trangthai")] BinhLuan binhLuan);
        ActionResult Edit(int? id);
        ActionResult Edit([Bind(Include = "mabinhluan,ten,noidung,vote,ngaybinhluan,matin,trangthai")] BinhLuan binhLuan);
        ActionResult Delete(int? id);
        ActionResult DeleteConfirmed(int id);
    }
}
