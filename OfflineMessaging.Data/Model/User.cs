using OfflineMessaging.Data.Model;
using System;

namespace OfflineMessaging.Data
{
    public class User : IEntity<string>
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
