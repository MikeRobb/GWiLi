using System.Web.Mvc;

namespace GWiLi.Areas.List
{
    public class ListAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "List";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            // My List
            context.MapRoute(
                "MyList_default",
                "MyList",
                new { controller = "List", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "List_default",
                "{controller}/{action}/{id}",
                new { controller = "List", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}