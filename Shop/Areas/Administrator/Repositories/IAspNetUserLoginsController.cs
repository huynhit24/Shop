using Shop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shop.Areas.Administrator.Repositories
{
    internal interface IAspNetUserLoginsController
    {
        ActionResult Index();
        ActionResult Details(string id);
        ActionResult Create();
        ActionResult Create([Bind(Include = "LoginProvider,ProviderKey,UserId")] AspNetUserLogin aspNetUserLogin);
        ActionResult Edit(string id);
        ActionResult Edit([Bind(Include = "LoginProvider,ProviderKey,UserId")] AspNetUserLogin aspNetUserLogin);
        ActionResult Delete(string id);
        ActionResult DeleteConfirmed(string id);
    }
}
