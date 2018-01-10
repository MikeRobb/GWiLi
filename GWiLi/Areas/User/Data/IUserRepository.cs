namespace GWiLi.Areas.User.Data
{
    public interface IUserRepository
    {
        bool ClaimUser(int userId, string email, string password);
        bool AttachEmail(EntityFramework.User user, string email, bool saveChanges = true);
        EntityFramework.User Login(string username, string password, string ip);
    }
}
