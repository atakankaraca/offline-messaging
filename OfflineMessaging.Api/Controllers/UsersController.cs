using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfflineMessaging.Api.Attributes;
using OfflineMessaging.Data;
using OfflineMessaging.Service;
using OfflineMessaging.Service.DTO;
using OfflineMessaging.Service.Helper;
using SharpRaven;
using System.Collections.Generic;

namespace OfflineMessaging.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ModelValidation]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly RavenClient _ravenClient;
        private readonly ValidateHelper _validator;

        public UsersController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
            _ravenClient = new RavenClient(_configuration.GetSection("SentryLogger").GetSection("LogDsn").Value);
            _validator = new ValidateHelper();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetUsers();
            if (users == null)
                return NotFound();

            var usersDto = _mapper.Map<List<UserInfoDto>>(users);
            return Ok(usersDto);
        }

        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserInfoDto>(user);
            return Ok(userDto);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Auth([FromBody]UserDto userDto)
        {
            var user = _userService.AuthenticateUser(userDto.Id, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenString = TokenGenerator.GenerateToken(user);

            return Ok(new { username = user.Id, fullname = user.FullName, token = tokenString });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromBody] UserDto user)
        {
            var validationResult = _validator.ValidateUserModel(user);
            if (!validationResult.IsValid)
                return BadRequest(new { message = validationResult.Errors });

            var userToAdd = _mapper.Map<User>(user);

            _userService.RegisterUser(userToAdd);
            return Ok();

        }

        [AllowAnonymous]
        [HttpGet("sentry")]
        public ActionResult TryLogger()
        {
            int i2 = 0;
            int i = 10 / i2;

            return Ok(i);
        }
    }
}
