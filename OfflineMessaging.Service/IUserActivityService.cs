using OfflineMessaging.Data.Model;
using System.Collections.Generic;

namespace OfflineMessaging.Service
{
    public interface IUserActivityService
    {
        IEnumerable<UserActivity> GetAllActivitiesOfCurrentUser();
        void CreateUserActivity(UserActivity userActivity);
    }
}
