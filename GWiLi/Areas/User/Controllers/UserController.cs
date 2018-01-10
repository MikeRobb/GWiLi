using GWiLi.Areas.User.Data;
using GWiLi.Areas.User.Models;
using GWiLi.Controllers;
using GWiLi.Core;
using GWiLi.EntityFramework;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GWiLi.Areas.User.Controllers
{
    public class UserController : BaseController
    {
        #region Properties

        private IUserRepository Repo;

        #endregion

        #region Constructors

        public UserController()
        {
            Repo = new UserRepository();
        }

        #endregion

        #region Methods

        public ActionResult MDR(int? id)
        {
            var username = @"mike.robb@vikings.berry.edu";
            var password = @"Password1234!@#$";
            switch(id)
            {
                case 1:
                    username = @"MIKE.ROBB@VIKINGS.BERRY.EDU";
                    break;
                case 2:
                    username = @"MROBB@VIKINGS.BERRY.EDU";
                    break;
                case 3:
                    password = @"MRobb";
                    break;
                case 4:
                    password = @"password1234!@#$";
                    break;
            }

            if(HandleLogin(Repo.Login(username, password, Request.UserHostAddress)))
            {
                return RedirectToAction("Index", "List");
            } else
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            var user = GetUser();
            if (user != null)
                return RedirectToAction("Index", "List");

            if (model.IsLoginAttempt())
            {
                if (HandleLogin(Repo.Login(model.Username, model.Password, Request.UserHostAddress)))
                {
                    return RedirectToAction("Index", "List");
                } else
                {
                    model.Success = false;
                    model.Message = "There was no username or password that matched";
                }
            }

            return View(model);
        }

        private bool HandleLogin(EntityFramework.User user)
        {
            var result = false;
            UserContext userContext = null;
            if (user != null)
            {
                result = true;
                userContext = new UserContext(user);
            }
            SetUser(userContext);

            return result;
        }

        #endregion
    }
}