using OfflineMessaging.Data;
using System.Collections.Generic;

namespace OfflineMessaging.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User AuthenticateUser(string username, string password);
        User GetCurrentUser();
        User GetUserByUsername(string username);
        bool IsUniqueUserName(string username);
        bool IsUniqueEmail(string email);
        void RegisterUser(User user);
    }
}
