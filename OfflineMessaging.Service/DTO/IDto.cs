using System;

namespace OfflineMessaging.Service.DTO
{
    public interface IDto <TKey>
        where TKey : IComparable
    {
        TKey Id { get; set; }

        DateTime CreatedDate { get; set; }
    }
}
