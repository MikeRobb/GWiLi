using System.Web.Mvc;

namespace GWiLi.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Login_default",
                "Login",
                new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "User_default",
                "{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}