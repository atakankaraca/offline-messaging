using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using OfflineMessaging.Data;
using OfflineMessaging.Repository;

namespace OfflineMessaging.Service
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message, int> _messageRepository;
        private readonly IBlockListService _blockListService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _currentUser;

        public MessageService(IRepository<Message, int> messageRepository, IBlockListService blockListService, IHttpContextAccessor httpContextAccessor)
        {
            _messageRepository = messageRepository;
            _blockListService = blockListService;
            _httpContextAccessor = httpContextAccessor;
            _currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }

        public IEnumerable<Message> GetAllSentMessagesOfUser()
        {
            if (string.IsNullOrEmpty(_currentUser))
                return null;

            return _messageRepository.GetByQuery(x=>x.SenderId == _currentUser);
        }

        public Message GetMessage(int id)
        {
            return _messageRepository.Get(id);
        }

        public IEnumerable<Message> GetMessagesByConversation(string receiverUsername)
        {

            if (string.IsNullOrEmpty(receiverUsername) || string.IsNullOrEmpty(_currentUser))
                return null;

            return _messageRepository
                .GetByQuery(x => 
                    (x.SenderId == _currentUser && x.ReceiverId == receiverUsername) || 
                    (x.SenderId == receiverUsername && x.ReceiverId == _currentUser))
                .OrderByDescending(y => y.CreatedDate);
        }

        public void SendMessage(Message message)
        {
            if (string.IsNullOrEmpty(_currentUser))
                return;

            var isUserBlocked = _blockListService.IsBlocked(message.SenderId, message.ReceiverId);            
            if (!isUserBlocked)
            {
                _messageRepository.Insert(message);
            }
        }
    }
}
