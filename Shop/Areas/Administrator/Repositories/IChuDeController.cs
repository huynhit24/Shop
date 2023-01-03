using Shop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shop.Areas.Administrator.Repositories
{
    internal interface IChuDeController
    {
        ActionResult Index();
        ActionResult Details(int? id);
        ActionResult Create();
        ActionResult Create([Bind(Include = "machude,tenchude,slug")] ChuDe chuDe);
        ActionResult Edit(int? id);
        ActionResult Edit([Bind(Include = "machude,tenchude,slug")] ChuDe chuDe);
        ActionResult Delete(int? id);
        ActionResult DeleteConfirmed(int id);
    }
}
