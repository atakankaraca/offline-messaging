using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OfflineMessaging.Data.Model;
using OfflineMessaging.Repository;

namespace OfflineMessaging.Service
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IRepository<UserActivity, int> _userActivityRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _currentUser;

        public UserActivityService(IRepository<UserActivity, int> userActivityRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userActivityRepository = userActivityRepository;
            _httpContextAccessor = httpContextAccessor;
            _currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }

        public void CreateUserActivity(UserActivity userActivity)
        {
            _userActivityRepository.Insert(userActivity);
        }

        public IEnumerable<UserActivity> GetAllActivitiesOfCurrentUser()
        {
            if (_currentUser == null)
                return null;

            return _userActivityRepository.GetByQuery(x => x.UserId == _currentUser);
        }
    }
}
