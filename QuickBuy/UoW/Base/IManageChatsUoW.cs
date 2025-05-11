using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickBuy.UoW.Base
{
    public interface IManageChatsUoW
    {
        Task<Chat> CreateChat(ChatDto chatDto);
        Task<Chat> GetChatById(int id);
        Task<List<Chat>> GetChatsByUserId(int userId);
        Task<bool> DeleteChat(int chatId);
    }
}
