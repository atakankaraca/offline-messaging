using OfflineMessaging.Data.Model;
using System;

namespace OfflineMessaging.Data
{
    public class Message : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string MessageString { get; set; }

        public virtual User From { get; set; }
        public virtual User To { get; set; }
    }
}