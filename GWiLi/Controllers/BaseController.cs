using GWiLi.EntityFramework;
using System.Web.Mvc;

namespace GWiLi.Controllers
{
    public class BaseController : Controller
    {
        public GWiLiEntities Database;

        public BaseController()
        {
            Database = new GWiLiEntities();
            ViewBag.Title = "GWiLi";
        }
    }
}