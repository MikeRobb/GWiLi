using GWiLi.Core;
using GWiLi.EntityFramework;
using GWiLi.EntityFramework.Extensions;
using System;
using System.Linq;

namespace GWiLi.Areas.User.Data
{
    public class UserRepository : IUserRepository
    {
        #region Properties

        private ICoreRepository _coreRepo;
        private GWiLiEntities _dataContext;

        #endregion

        #region Constructors

        public UserRepository()
        {
            _dataContext = new GWiLiEntities();
            _coreRepo = new CoreRepository(_dataContext);
        }

        #endregion

        #region Methods

        public bool ClaimUser(int userId, string email, string password)
        {
            try
            {
                var user = _dataContext.Users.FirstOrDefault(x => x.Id == userId);

                // No user found
                if (user == null)
                    throw new Exception($"User {userId} does not exisit");

                // User already claimed
                if (user.HasBeenClaimed())
                {
                    var dateClaimed = user.UserEmails.OrderBy(x => x.DateAdded).First().DateAdded;
                    throw new Exception($"User {user.GetDisplayName()} has already been claimed on {dateClaimed.ToString("hh:mm tt MM/dd/yy")}");
                }

                var salt = EntityFramework.User.GenerateSalt();
                var hash = EntityFramework.User.GenerateHash(password, salt);
                user.Salt = salt;
                user.Hash = hash;
                AttachEmail(user, email, false);
                _dataContext.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                FileLog.Instance.WriteLine(e.GetFullExceptionMessage($"Claiming User {userId} failed!"));
                return false;
            }
        }

        public bool AttachEmail(EntityFramework.User user, string email, bool saveChanges = true)
        {
            try
            {
                _dataContext.UserEmails.Add(new UserEmail
                {
                    User = user,
                    Email = email.ToLowerInvariant(),
                    DateAdded = DateTime.Now,
                });
                if (saveChanges)
                    _dataContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                FileLog.Instance.WriteLine(e.GetFullExceptionMessage($"Failed to attach email {email} to user {user.GetDisplayName()}."));
            }
            return false;
        }

        
        public EntityFramework.User Login(string username, string password, string ip)
        {
            EntityFramework.User result = null;
            try
            {
                var attempt = new LoginAttempt
                {
                    Username = username,
                    Password = "<withheld>",
                    IP = ip,
                    Attempt = DateTime.Now,
                    Success = true,
                };

                try
                {
                    username = username.ToLowerInvariant();
                    var userEmail = _dataContext.UserEmails.SingleOrDefault(x => x.Email == username);
                    if (userEmail == null)
                        throw new Exception($"There are no users for {username}");

                    var userSalt = userEmail.User.Salt;
                    var userHash = userEmail.User.Hash;

                    var attemptHash = EntityFramework.User.GenerateHash(password, userSalt);
                    if (!userHash.SequenceEqual(attemptHash))
                        throw new Exception($"The passwords do not match");
                
                    result = userEmail.User;
                }
                catch (Exception queryException)
                {
                    FileLog.Instance.WriteLine(queryException.GetFullExceptionMessage("Invalid Login"));
                    attempt.Success = false;
                    attempt.Password = password;
                } finally
                {
                    _dataContext.LoginAttempts.Add(attempt);
                    _dataContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                FileLog.Instance.WriteLine(e.GetFullExceptionMessage($"Failed to persist LoginAttempt {username} // {password} from {ip}"));
            }

            return result;
        }
        #endregion
    }
}