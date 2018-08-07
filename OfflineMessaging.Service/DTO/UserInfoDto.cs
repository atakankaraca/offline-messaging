using System;

namespace OfflineMessaging.Service.DTO
{
    public class UserInfoDto : IDto<string>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
