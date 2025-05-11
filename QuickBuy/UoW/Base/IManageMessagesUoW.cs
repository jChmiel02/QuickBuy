using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickBuy.UoW.Base
{
    public interface IManageMessagesUoW
    {
        Task<Message> SendMessage(MessageDto messageDto);
        Task<List<Message>> GetMessagesByChatId(int chatId);
        Task<bool> DeleteMessage(int messageId);
    }
}
