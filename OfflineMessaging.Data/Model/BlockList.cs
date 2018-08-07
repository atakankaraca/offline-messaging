using OfflineMessaging.Data.Model;
using System;

namespace OfflineMessaging.Data
{
    public class BlockList : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BlockerId { get; set; }
        public string BlockedId { get; set; }

        public virtual User Blocker { get; set; }
        public virtual User Blocked { get; set; }
    }
}
