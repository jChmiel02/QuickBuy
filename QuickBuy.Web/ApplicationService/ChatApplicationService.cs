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
    public class ChatApplicationService : IChatApplicationService
    {
        private readonly IManageChatsUoW _manageChatsUoW;
        private readonly IManageItemsUoW _manageItemsUoW;
        private readonly IMapper _mapper;
        private readonly ILogger<ChatApplicationService> _logger;

        public ChatApplicationService(
            IManageChatsUoW manageChatsUoW,
            IManageItemsUoW manageItemsUoW,
            IMapper mapper,
            ILogger<ChatApplicationService> logger)
        {
            _manageChatsUoW = manageChatsUoW;
            _manageItemsUoW = manageItemsUoW;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> CreateChat(ChatDto chatDto)
        {
            try
            {
                var item = await _manageItemsUoW.GetItemById(chatDto.ItemId);
                if (item == null)
                {
                    return new BadRequestObjectResult("Item with the given ID does not exist.");
                }

                var chat = await _manageChatsUoW.CreateChat(chatDto);
                var resultDto = _mapper.Map<ChatDto>(chat);
                return new OkObjectResult(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating chat for ItemId {chatDto.ItemId}");
                return new ObjectResult("An error occurred while creating the chat.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetChatById(int id)
        {
            try
            {
                var chat = await _manageChatsUoW.GetChatById(id);
                if (chat == null)
                {
                    return new NotFoundObjectResult("Chat not found.");
                }

                var resultDto = _mapper.Map<ChatDto>(chat);
                return new OkObjectResult(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving chat with ID {id}");
                return new ObjectResult("An error occurred while retrieving the chat.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetChatsByUserId(int userId)
        {
            try
            {
                var chats = await _manageChatsUoW.GetChatsByUserId(userId);
                if (chats == null || chats.Count == 0)
                {
                    return new NotFoundObjectResult("No chats found for this user.");
                }

                var resultDtos = _mapper.Map<List<ChatDto>>(chats);
                return new OkObjectResult(resultDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving chats for user ID {userId}");
                return new ObjectResult("An error occurred while retrieving chats.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> DeleteChat(int chatId)
        {
            try
            {
                var deleted = await _manageChatsUoW.DeleteChat(chatId);
                if (deleted)
                {
                    return new OkObjectResult("Chat deleted successfully.");
                }

                return new NotFoundObjectResult("Chat not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting chat with ID {chatId}");
                return new ObjectResult("An error occurred while deleting the chat.") { StatusCode = 500 };
            }
        }
    }
}
