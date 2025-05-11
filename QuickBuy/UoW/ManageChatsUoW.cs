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
    public class ManageChatsUoW : IManageChatsUoW
    {
        private readonly QuickBuyDbContext _context;
        private readonly IMapper _mapper;

        public ManageChatsUoW(QuickBuyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Chat> CreateChat(ChatDto chatDto)
        {
            var chat = _mapper.Map<Chat>(chatDto);
            try
            {
                _context.Chats.Add(chat);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return chat;
        }

        public async Task<Chat> GetChatById(int id)
        {
            return await _context.Chats
                .Include(c => c.Item)
                .Include(c => c.Buyer)
                .Include(c => c.Seller)
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Chat>> GetChatsByUserId(int userId)
        {
            return await _context.Chats
                .Where(c => c.BuyerId == userId || c.SellerId == userId)
                .Include(c => c.Item)
                .Include(c => c.Messages)
                .ToListAsync();
        }

        public async Task<bool> DeleteChat(int chatId)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(c => c.Id == chatId);
            if (chat == null)
            {
                return false;
            }

            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
