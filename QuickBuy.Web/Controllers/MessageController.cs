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
    public class MessageController : Controller
    {
        private readonly IMessageApplicationService _messageApplicationService;

        public MessageController(IMessageApplicationService messageApplicationService)
        {
            _messageApplicationService = messageApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            return await _messageApplicationService.SendMessage(messageDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesByChatId([FromQuery] int chatId)
        {
            return await _messageApplicationService.GetMessagesByChatId(chatId);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessage([FromQuery] int messageId)
        {
            return await _messageApplicationService.DeleteMessage(messageId);
        }
    }

}
