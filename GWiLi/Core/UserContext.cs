
using System;

namespace GWiLi.Core
{
    public class UserContext
    {
        public static string UserCookieKey = "GWL_UserContext";

        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public DateTime LoggedInAt { get; set; }

        public UserContext(EntityFramework.User user)
        {
            UserId = user.Id;
            DisplayName = user.GetDisplayName();
            LoggedInAt = DateTime.Now;
        }
    }
}