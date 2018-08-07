using OfflineMessaging.Data;
using System.Collections.Generic;

namespace OfflineMessaging.Service
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAllSentMessagesOfUser();
        IEnumerable<Message> GetMessagesByConversation(string secondUsername);
        Message GetMessage(int id);
        void SendMessage(Message message);
    }
}
