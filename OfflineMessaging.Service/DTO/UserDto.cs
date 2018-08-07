using System;

namespace OfflineMessaging.Service.DTO
{
    public class UserDto : IDto<string>
    {
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public UserDto()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
