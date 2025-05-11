using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace QuickBuy.WEB.ApplicationServices.Base
{
    public interface IItemApplicationService
    {
        Task<IActionResult> CreateItem(ItemDto itemDto);
        Task<IActionResult> GetItemById(int id);
        Task<IActionResult> GetItemsBySellerId(int sellerId);
        Task<IActionResult> MarkItemAsSold(int itemId);
        Task<IActionResult> GetAllItems();
    }
}
