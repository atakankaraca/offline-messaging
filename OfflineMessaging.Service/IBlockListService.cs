using OfflineMessaging.Data;
using System.Collections.Generic;

namespace OfflineMessaging.Service
{
    public interface IBlockListService
    {
        IEnumerable<BlockList> GetBlockedListOfCurrentUser();
        bool IsBlocked(string blocked, string blocker);
        void BlockUser(BlockList blockingModel);
    }
}
