using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickBuy.UoW.Base
{
    public interface IManageItemsUoW
    {
        Task<Item> CreateItem(ItemDto itemDto);
        Task<Item> GetItemById(int id);
        Task<List<Item>> GetItemsBySellerId(int sellerId);
        Task<bool> MarkItemAsSold(int itemId);
        Task<List<Item>> GetAllItems();

    }
}
