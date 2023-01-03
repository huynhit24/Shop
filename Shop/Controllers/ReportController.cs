using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public EmptyResult Export(string GridHtml)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Grid.docx");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word";
            Response.Output.Write(GridHtml);
            Response.Flush();
            Response.End();
            return new EmptyResult();
        }
    }
}