using Shop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shop.Areas.Administrator.Repositories
{
    internal interface IAspNetUsersController
    {
        ActionResult Index();
        ActionResult Details(string id);
        ActionResult Create();
        ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,ngaysinh,profile,avatar,hoten,diachi")] AspNetUser aspNetUser);
        ActionResult Edit(string id);
        ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,ngaysinh,profile,avatar,hoten,diachi")] AspNetUser aspNetUser);
        ActionResult Delete(string id);
        ActionResult DeleteConfirmed(string id);
        ActionResult DelTrashAcc(string id);
        ActionResult UndoTrashAcc(string id);

    }
}
