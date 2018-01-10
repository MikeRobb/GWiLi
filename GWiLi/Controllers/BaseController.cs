using GWiLi.Core;
using GWiLi.EntityFramework;
using System.Web.Mvc;

namespace GWiLi.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ViewBag.Title = "GWiLi";
        }

        public UserContext GetUser()
        {
            return (UserContext)Session[UserContext.UserCookieKey];
        }

        public void SetUser(UserContext user)
        {
            Session[UserContext.UserCookieKey] = user;
        }

        public void Logout()
        {
            SetUser(null);
        }
    }
}