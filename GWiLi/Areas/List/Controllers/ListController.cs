using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GWiLi.Areas.List.Controllers
{
    public class ListController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllMyLists()
        {
            return View("Index");
        }
    }
}