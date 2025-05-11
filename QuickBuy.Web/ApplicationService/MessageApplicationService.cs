using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using QuickBuy.UoW.Base;
using QuickBuy.WEB.ApplicationServices.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickBuy.WEB.ApplicationServices
{
    public class MessageApplicationService : IMessageApplicationService
    {
        private readonly IManageMessagesUoW _manageMessagesUoW;
        private readonly IMapper _mapper;
        private readonly ILogger<MessageApplicationService> _logger;

        public MessageApplicationService(
            IManageMessagesUoW manageMessagesUoW,
            IMapper mapper,
            ILogger<MessageApplicationService> logger)
        {
            _manageMessagesUoW = manageMessagesUoW;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> SendMessage(MessageDto messageDto)
        {
            try
            {
                var message = await _manageMessagesUoW.SendMessage(messageDto);
                var resultDto = _mapper.Map<MessageDto>(message);
                return new OkObjectResult(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while sending message in chat {messageDto.ChatId}");
                return new ObjectResult("An error occurred while sending the message.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetMessagesByChatId(int chatId)
        {
            try
            {
                var messages = await _manageMessagesUoW.GetMessagesByChatId(chatId);
                if (messages == null || messages.Count == 0)
                {
                    return new NotFoundObjectResult("No messages found for this chat.");
                }

                var resultDto = _mapper.Map<List<MessageDto>>(messages);
                return new OkObjectResult(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving messages for chat {chatId}");
                return new ObjectResult("An error occurred while retrieving messages.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            try
            {
                bool deleted = await _manageMessagesUoW.DeleteMessage(messageId);
                if (deleted)
                {
                    return new OkObjectResult("Message deleted successfully.");
                }

                return new NotFoundObjectResult("Message not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting message with ID {messageId}");
                return new ObjectResult("An error occurred while deleting the message.") { StatusCode = 500 };
            }
        }
    }
}
