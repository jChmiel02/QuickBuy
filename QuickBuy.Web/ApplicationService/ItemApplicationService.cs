using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using QuickBuy.UoW.Base;
using QuickBuy.WEB.ApplicationServices.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickBuy.WEB.ApplicationServices
{
    public class ItemApplicationService : IItemApplicationService
    {
        private readonly IManageItemsUoW _manageItemsUoW;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemApplicationService> _logger;

        public ItemApplicationService(IManageItemsUoW manageItemsUoW, IMapper mapper, ILogger<ItemApplicationService> logger)
        {
            _manageItemsUoW = manageItemsUoW;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> CreateItem(ItemDto itemDto)
        {
            try
            {
                var item = await _manageItemsUoW.CreateItem(itemDto);
                var resultDto = _mapper.Map<ItemDto>(item);
                return new OkObjectResult(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating item.");
                return new ObjectResult("An error occurred while creating the item.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetItemById(int id)
        {
            try
            {
                var item = await _manageItemsUoW.GetItemById(id);
                if (item == null)
                {
                    return new NotFoundObjectResult("Item not found.");
                }
                var resultDto = _mapper.Map<ItemDto>(item);
                return new OkObjectResult(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving item with ID {id}.");
                return new ObjectResult("An error occurred while retrieving the item.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetItemsBySellerId(int sellerId)
        {
            try
            {
                var items = await _manageItemsUoW.GetItemsBySellerId(sellerId);
                if (items == null || items.Count == 0)
                {
                    return new NotFoundObjectResult("No items found for this seller.");
                }
                var resultDtos = _mapper.Map<List<ItemDto>>(items);
                return new OkObjectResult(resultDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving items for seller ID {sellerId}.");
                return new ObjectResult("An error occurred while retrieving items for the seller.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> MarkItemAsSold(int itemId)
        {
            try
            {
                bool success = await _manageItemsUoW.MarkItemAsSold(itemId);
                if (success)
                {
                    return new OkObjectResult("Item marked as sold.");
                }
                return new NotFoundObjectResult("Item not found or already sold.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while marking item ID {itemId} as sold.");
                return new ObjectResult("An error occurred while marking the item as sold.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var items = await _manageItemsUoW.GetAllItems();
                if (items == null || items.Count == 0)
                {
                    return new NotFoundObjectResult("No items found.");
                }
                var resultDtos = _mapper.Map<List<ItemDto>>(items);
                return new OkObjectResult(resultDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all items.");
                return new ObjectResult("An error occurred while retrieving the items.") { StatusCode = 500 };
            }
        }
    }
}
