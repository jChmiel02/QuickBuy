using AutoMapper;
using QuickBuy.Database.DbContext;
using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using QuickBuy.UoW.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickBuy.UoW
{
    public class ManageItemsUoW : IManageItemsUoW
    {
        private readonly QuickBuyDbContext _context;
        private readonly IMapper _mapper;

        public ManageItemsUoW(QuickBuyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Item> CreateItem(ItemDto itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);
            try
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return item;
        }

        public async Task<Item> GetItemById(int id)
        {
            return await _context.Items
                .Include(i => i.Seller)
                .Include(i => i.Transaction)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Item>> GetItemsBySellerId(int sellerId)
        {
            return await _context.Items
                .Where(i => i.SellerId == sellerId)
                .Include(i => i.Transaction)
                .ToListAsync();
        }

        public async Task<bool> MarkItemAsSold(int itemId)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
            if (item == null || item.IsSold)
            {
                return false;
            }

            item.IsSold = true;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<User> GetUserById(int sellerId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == sellerId);
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await _context.Items
                .Include(i => i.Seller)
                .Include(i => i.Transaction)
                .ToListAsync();
        }

    }
}
