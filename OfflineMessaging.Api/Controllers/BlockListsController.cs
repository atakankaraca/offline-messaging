using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfflineMessaging.Api.Attributes;
using OfflineMessaging.Data;
using OfflineMessaging.Service;
using OfflineMessaging.Service.DTO;
using OfflineMessaging.Service.Helper;

namespace OfflineMessaging.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ModelValidation]
    public class BlockListsController : Controller
    {
        private readonly IBlockListService _blockListService;
        private readonly IMapper _mapper;
        private readonly ValidateHelper _validator;

        public BlockListsController(IBlockListService blockListService, IMapper mapper)
        {
            _blockListService = blockListService;
            _mapper = mapper;
            _validator = new ValidateHelper();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userBlockList = _blockListService.GetBlockedListOfCurrentUser();
            if (userBlockList == null)
                return NotFound();

            var userBlockListDto = _mapper.Map<BlockListDto>(userBlockList);
            return Ok(userBlockListDto);
        }

        [HttpPost]
        public ActionResult Post([FromBody] BlockListDto block)
        {
            var validationResult = _validator.ValidateBlockListModel(block);
            if (!validationResult.IsValid)
                return BadRequest(new { message = validationResult.Errors });

            var blockModel = _mapper.Map<BlockList>(block);

            _blockListService.BlockUser(blockModel);
            return Ok();

        }
    }
}
