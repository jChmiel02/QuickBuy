using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickBuy.Database.Models.Dto;
using QuickBuy.WEB.ApplicationServices.Base;
using System;
using System.Threading.Tasks;

namespace QuickBuy.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ItemController : Controller
    {
        private readonly IItemApplicationService _itemApplicationService;

        public ItemController(IItemApplicationService itemApplicationService)
        {
            _itemApplicationService = itemApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemDto itemDto)
        {
            return await _itemApplicationService.CreateItem(itemDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetItemById([FromQuery] int id)
        {
            return await _itemApplicationService.GetItemById(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetItemsBySellerId([FromQuery] int sellerId)
        {
            return await _itemApplicationService.GetItemsBySellerId(sellerId);
        }

        [HttpPatch]
        public async Task<IActionResult> MarkItemAsSold([FromQuery] int itemId)
        {
            return await _itemApplicationService.MarkItemAsSold(itemId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            return await _itemApplicationService.GetAllItems();
        }
    }

}
