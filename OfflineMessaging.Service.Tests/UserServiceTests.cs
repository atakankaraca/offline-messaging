using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using OfflineMessaging.Data;
using OfflineMessaging.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Xunit;

namespace OfflineMessaging.Service.Tests
{
    public class UserServiceTests
    {
        //public class UserComparer : Comparer<User>
        //{
        //    public override int Compare(User x, User y)
        //    {
        //        return x.Id.Equals(y.Id) &&
        //                x.Password.Equals(y.Password) &&
        //                x.CreatedDate.Equals(y.CreatedDate) &&
        //                x.Email.Equals(y.Email) &&
        //                x.FullName.Equals(y.FullName) ? 0 : 1;
        //    }
        //}

        //private List<User> _usersEntityList = new List<User>
        //{
        //    new User
        //    {
        //        Id = "atakankaraca",
        //        FullName = "Atakan Karaca",
        //        CreatedDate = DateTime.Parse("2018-08-07 13:54:23.0071587"),
        //        Email = "atkn.karaca@gmail.com",
        //        Password = "1234567"
        //    },
        //    new User
        //    {
        //        Id = "akaraca",
        //        FullName = "A Karaca",
        //        CreatedDate = DateTime.Parse("2018-08-07 13:54:23.0071587"),
        //        Email = "a.karaca@gmail.com",
        //        Password = "1234567"
        //    },
        //    new User
        //    {
        //        Id = "bkaraca",
        //        FullName = "B Karaca",
        //        CreatedDate = DateTime.Parse("2018-08-07 13:54:23.0071587"),
        //        Email = "b.karaca@gmail.com",
        //        Password = "1234567"
        //    }
        //};

        //private UserService _userService;
        //private Mock<IRepository<User, string>> _mockRepository;
        //private Mock<IUserActivityService> _mockService;
        //private Mock<IHttpContextAccessor> _mockContext;
        //private string _currentUser;

        //public UserServiceTests()
        //{
        //    _mockService = new Mock<IUserActivityService>();
        //    _mockContext = new Mock<IHttpContextAccessor>();
        //    _mockRepository = new Mock<IRepository<User, string>>();
        //    var fakeIdentity = new GenericIdentity("atakankaraca");
        //    _currentUser = fakeIdentity.Name;

        //    _userService = new UserService(_mockRepository.Object,_mockService.Object, _mockContext.Object);
        //}

        //[Fact]
        //public void TestGetAll()
        //{
        //    var users = _userService.GetUsers().ToList();
        //    var expectedUsers = _usersEntityList;

        //    Assert.True(users.Equals(expectedUsers));
        //}

        //[Fact]
        //public void TestGetCurrentUser()
        //{
        //    var user = _userService.GetCurrentUser();
        //    var expectedUser = _usersEntityList.FirstOrDefault();

        //    Assert.True(user.Equals(expectedUser));
        //}

        //[Fact]
        //public void TestGetUserByUsername()
        //{
        //    var user = _userService.GetUserByUsername("akaraca");
        //    var expectedUser = _usersEntityList[1];

        //    Assert.True(user.Equals(expectedUser));
        //}

        //[Fact]
        //public void TestRegisterUser()
        //{
        //    var user = new User
        //    {
        //        Id = "bkaraca",
        //        FullName = "B Karaca",
        //        CreatedDate = DateTime.Parse("2018-08-07 13:54:23.0071587"),
        //        Email = "b.karaca@gmail.com",
        //        Password = "1234567"
        //    };

        //    _userService.RegisterUser(user);
        //    Assert.True(user.Equals(_usersEntityList.LastOrDefault()));
        //}
    }
}
