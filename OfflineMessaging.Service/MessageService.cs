using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using OfflineMessaging.Data;
using OfflineMessaging.Repository;
using OfflineMessaging.Service.Helper;

namespace OfflineMessaging.Service
{
    public class MessageService : IMessageService
    {
        private IRepository<Message, int> _messageRepository;
        private IBlockListService _blockListService;
        private IHttpContextAccessor _httpContextAccessor;
        private string _currentUser;

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

            message.SenderId = _currentUser;
            var isUserBlocked = _blockListService.IsBlocked(message.SenderId, message.ReceiverId);
            var isValidMessageModel = ValidateHelper.ValidateMessageModel(message);

            if (!isUserBlocked && isValidMessageModel)
            {
                _messageRepository.Insert(message);
            }
        }
    }
}
