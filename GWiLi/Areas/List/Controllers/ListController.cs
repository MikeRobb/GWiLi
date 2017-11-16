using GWiLi.Controllers;
using GWiLi.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace GWiLi.Areas.List.Controllers
{
    public class ListController : BaseController
    {
        public ActionResult Index()
        {
            var lists = Database.List.ToList();

            return View();
        }
    }
}