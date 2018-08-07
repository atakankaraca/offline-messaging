using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfflineMessaging.Data;
using OfflineMessaging.Service;
using OfflineMessaging.Service.DTO;
using System.Collections.Generic;

namespace OfflineMessaging.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sentMessages = _messageService.GetAllSentMessagesOfUser();
            return Ok(_mapper.Map<List<MessageDto>>(sentMessages));
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            var message = _messageService.GetMessage(id);
            return Ok(_mapper.Map<MessageDto>(message));
        }

        [HttpGet("conversations/{username}")]
        public IActionResult GetMessagesByConversation(string username)
        {
            var message = _messageService.GetMessagesByConversation(username);
            return Ok(_mapper.Map<List<MessageDto>>(message));
        }

        [HttpPost]
        public ActionResult Post([FromBody] MessageDto message)
        {
            var messageToSend = _mapper.Map<Message>(message);
            _messageService.SendMessage(messageToSend);
            return Ok();
        }

    }
}
