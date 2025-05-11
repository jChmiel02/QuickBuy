using AutoMapper;
using QuickBuy.Database.DbContext;
using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using QuickBuy.UoW.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace QuickBuy.UoW
{
    public class ManageMessagesUoW : IManageMessagesUoW
    {
        private readonly QuickBuyDbContext _context;
        private readonly IMapper _mapper;

        public ManageMessagesUoW(QuickBuyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Message> SendMessage(MessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            try
            {
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return message;
        }

        public async Task<List<Message>> GetMessagesByChatId(int chatId)
        {
            return await _context.Messages
                .Include(m => m.Chat)
                .Include(m => m.Sender)
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task<bool> DeleteMessage(int messageId)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
            if (message == null)
            {
                return false;
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
