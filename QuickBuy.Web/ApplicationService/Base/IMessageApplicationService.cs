using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models.Dto;
using System.Threading.Tasks;

namespace QuickBuy.WEB.ApplicationServices.Base
{
    public interface IMessageApplicationService
    {
        Task<IActionResult> SendMessage(MessageDto messageDto);
        Task<IActionResult> GetMessagesByChatId(int chatId);
        Task<IActionResult> DeleteMessage(int messageId);
    }
}
