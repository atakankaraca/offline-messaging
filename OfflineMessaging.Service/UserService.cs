using Microsoft.AspNetCore.Http;
using OfflineMessaging.Data;
using OfflineMessaging.Data.Model;
using OfflineMessaging.Repository;
using OfflineMessaging.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OfflineMessaging.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, string> _userRepository;
        private readonly IUserActivityService _userActivityService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _currentUser;

        public UserService(IRepository<User, string> userRepository, IUserActivityService userActivityService, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userActivityService = userActivityService;
            _httpContextAccessor = httpContextAccessor;
            _currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll().ToList();
        }

        IEnumerable<User> GetUsersByQuery(Expression<Func<User, bool>> query)
        {
            return _userRepository.GetByQuery(query);
        }

        public User GetCurrentUser()
        {          
            return GetUsersByQuery(x => x.Id == _currentUser).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.Get(username);
        }

        public User AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return null;

            var user = GetUserByUsername(username);            
            if (user == null)
                return null;

            var isPasswordCorrect = user.Password == password;
            if (!isPasswordCorrect)
            {
                _userActivityService.CreateUserActivity(new UserActivity {
                    UserId = user.Id,
                    ActivityDetail = "Invalid Login Attempt."
                });

                return null;
            }

            _userActivityService.CreateUserActivity(new UserActivity
            {
                UserId = user.Id,
                ActivityDetail = "Login Sucessfull."
            });
            
            return user;
        }

        public void RegisterUser(User user)
        {
            if (!IsUniqueEmail(user.Email) || !IsUniqueUserName(user.Id))
                return;

            var isValidModel = ValidateHelper.ValidateUserModel(user);

            if (!isValidModel)                
                return;

            _userRepository.Insert(user);
        }
        
        public bool IsUniqueUserName(string username)
        {
            var user = GetUserByUsername(username);
            if(user != null)
            {
                return false;
            }
            return true;
        }

        public bool IsUniqueEmail(string email)
        {
            var user = GetUsersByQuery(x => x.Email == email).FirstOrDefault();
            if (user != null)
            {
                return false;
            }
            return true;
        }
    }
}
