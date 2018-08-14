using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfflineMessaging.Api.Attributes;
using OfflineMessaging.Service;
using OfflineMessaging.Service.DTO;
using System.Collections.Generic;

namespace OfflineMessaging.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ModelValidation]
    public class UserActivitiesController : Controller
    {
        private readonly IUserActivityService _userActivityService;
        private readonly IMapper _mapper;

        public UserActivitiesController(IUserActivityService userActivityService, IMapper mapper)
        {
            _userActivityService = userActivityService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userActivities = _userActivityService.GetAllActivitiesOfCurrentUser();
            if (userActivities == null)
                return NotFound();

            var userActivitiesDto = _mapper.Map<List<UserActivityDto>>(userActivities);

            return Ok(userActivitiesDto);
        }
    }
}
