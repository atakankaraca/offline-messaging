using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfflineMessaging.Data;
using OfflineMessaging.Service;
using OfflineMessaging.Service.DTO;

namespace OfflineMessaging.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class BlockListsController : Controller
    {
        private readonly IBlockListService _blockListService;
        private IMapper _mapper;

        public BlockListsController(IBlockListService blockListService, IMapper mapper)
        {
            _blockListService = blockListService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userBlockList = _blockListService.GetBlockedListOfCurrentUser();
            var userBlockListDto = _mapper.Map<BlockListDto>(userBlockList);

            return Ok(userBlockListDto);
        }

        [HttpPost]
        public ActionResult Post([FromBody] BlockListDto block)
        {

            var blockModel = _mapper.Map<BlockList>(block);
            _blockListService.BlockUser(blockModel);

            return Ok();

        }
    }
}
