using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickBuy.Database.Models.Dto;
using QuickBuy.WEB.ApplicationServices.Base;
using System;
using System.Threading.Tasks;

namespace QuickBuy.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ChatController : Controller
    {
        private readonly IChatApplicationService _chatApplicationService;

        public ChatController(IChatApplicationService chatApplicationService)
        {
            _chatApplicationService = chatApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] ChatDto chatDto)
        {
            return await _chatApplicationService.CreateChat(chatDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetChatById([FromQuery] int id)
        {
            return await _chatApplicationService.GetChatById(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetChatsByUserId([FromQuery] int userId)
        {
            return await _chatApplicationService.GetChatsByUserId(userId);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteChat([FromQuery] int chatId)
        {
            return await _chatApplicationService.DeleteChat(chatId);
        }
    }

}
