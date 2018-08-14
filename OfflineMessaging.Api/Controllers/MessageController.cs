using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfflineMessaging.Api.Attributes;
using OfflineMessaging.Data;
using OfflineMessaging.Service;
using OfflineMessaging.Service.DTO;
using OfflineMessaging.Service.Helper;
using System.Collections.Generic;

namespace OfflineMessaging.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ModelValidation]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ValidateHelper _validator;
        private readonly string _currentUser;

        public MessageController(IMessageService messageService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _messageService = messageService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _validator = new ValidateHelper();
            _currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sentMessages = _messageService.GetAllSentMessagesOfUser();
            if (sentMessages == null)
                return NotFound();
            return Ok(_mapper.Map<List<MessageDto>>(sentMessages));
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            var message = _messageService.GetMessage(id);
            if (message == null)
                return NotFound();

            return Ok(_mapper.Map<MessageDto>(message));
        }

        [HttpGet("conversations/{username}")]
        public IActionResult GetMessagesByConversation(string username)
        {
            var messages = _messageService.GetMessagesByConversation(username);
            if (messages == null)
                return NotFound();

            return Ok(_mapper.Map<List<MessageDto>>(messages));
        }

        [HttpPost]
        public ActionResult Post([FromBody] MessageDto message)
        {
            var validationResult = _validator.ValidateMessageModel(message);
            if (!validationResult.IsValid)
                return BadRequest(new { message = validationResult.Errors });

            message.SenderId = _currentUser;
            var messageToSend = _mapper.Map<Message>(message);

            _messageService.SendMessage(messageToSend);
            return Ok();
        }
    }
}
