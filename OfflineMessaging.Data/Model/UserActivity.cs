using System;

namespace OfflineMessaging.Data.Model
{
    public class UserActivity : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ActivityDetail { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public UserActivity()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
