using System;

namespace OfflineMessaging.Data.Model
{
    public interface IEntity<TKey>
        where TKey : IComparable
    {
        TKey Id { get; set; }

        DateTime CreatedDate { get; set; }
    }
}
