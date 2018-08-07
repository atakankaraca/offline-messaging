using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using OfflineMessaging.Data;
using OfflineMessaging.Repository;
using OfflineMessaging.Service.Helper;

namespace OfflineMessaging.Service
{
    public class BlockListService : IBlockListService
    {
        private IRepository<BlockList, int> _blockListRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private string _currentUser;

        public BlockListService(IRepository<BlockList, int> blockListRepository, IHttpContextAccessor httpContextAccessor)
        {
            _blockListRepository = blockListRepository;
            _httpContextAccessor = httpContextAccessor;
            _currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }

        public IEnumerable<BlockList> GetBlockedListOfCurrentUser()
        {
            if (_currentUser == null)
                return null;

            return _blockListRepository.GetByQuery(x => x.BlockerId == _currentUser).ToList();
        }

        public void BlockUser(BlockList blockListModel)
        {
            if (_currentUser != null)
                return;

            blockListModel.BlockerId = _currentUser;
            var isValidModel = ValidateHelper.ValidaBlockListModel(blockListModel);

            if (!isValidModel)
                return;
            if (IsBlocked(blockListModel.BlockerId, blockListModel.BlockerId))
                return;

            _blockListRepository.Insert(blockListModel);

        }

        public bool IsBlocked(string blocked, string blocker)
        {
            var blockedUser = _blockListRepository.GetByQuery(x => x.BlockerId == blocker && x.BlockedId == blocked).FirstOrDefault();

            if(blockedUser != null)
            {
                return true;
            }

            return false;
        }
    }
}
