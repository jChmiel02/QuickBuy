using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickBuy.WEB.ApplicationServices.Base
{
    public interface IChatApplicationService
    {
        Task<IActionResult> CreateChat(ChatDto chatDto);
        Task<IActionResult> GetChatById(int id);
        Task<IActionResult> GetChatsByUserId(int userId);
        Task<IActionResult> DeleteChat(int chatId);
    }
}
