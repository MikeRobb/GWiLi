using GWiLi.Models;

namespace GWiLi.Areas.User.Models
{
    public class LoginModel : BaseActionResultModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsLoginAttempt()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }
    }
}