using Shop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shop.Areas.Administrator.Repositories
{
    internal interface IAspNetRolesController
    {
        ActionResult Index();
        ActionResult Details(string id);
        ActionResult Create();
        ActionResult Create([Bind(Include = "Id,Name")] AspNetRole aspNetRole);
        ActionResult Edit(string id);
        ActionResult Edit([Bind(Include = "Id,Name")] AspNetRole aspNetRole);
        ActionResult Delete(string id);
        ActionResult DeleteConfirmed(string id);
    }
}
