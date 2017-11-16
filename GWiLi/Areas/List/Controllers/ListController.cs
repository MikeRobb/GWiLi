using GWiLi.Areas.List.Models;
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

            var model = new ListViewModel();
            model.Populate(lists);
            return View(model);
        }
    }
}