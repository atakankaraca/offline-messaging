using System;

namespace OfflineMessaging.Service.DTO
{
    public class BlockListDto : IDto<int>
    {
        public BlockListDto()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public string BlockerId { get; set; }
        public string BlockedId { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
