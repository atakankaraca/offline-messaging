using System;

namespace OfflineMessaging.Service.DTO
{
    public class MessageDto : IDto<int>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string MessageString { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public MessageDto()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
