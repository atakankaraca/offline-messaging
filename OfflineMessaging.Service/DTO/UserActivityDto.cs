using System;

namespace OfflineMessaging.Service.DTO
{
    public class UserActivityDto : IDto<int>
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ActivityDetail { get; set; }
        public string UserId { get; set; }
    }
}
