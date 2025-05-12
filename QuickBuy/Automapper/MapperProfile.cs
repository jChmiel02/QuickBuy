using AutoMapper;
using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuickBuy.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<ItemDto, Item>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Seller, opt => opt.Ignore());

            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.BuyerId))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<TransactionDto, Transaction>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.BuyerId))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Item, opt => opt.Ignore())
                .ForMember(dest => dest.Buyer, opt => opt.Ignore())
                .ForMember(dest => dest.Seller, opt => opt.Ignore());

            CreateMap<Chat, ChatDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.BuyerId))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId));

            CreateMap<ChatDto, Chat>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.BuyerId))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId))
                .ForMember(dest => dest.Item, opt => opt.Ignore())
                .ForMember(dest => dest.Buyer, opt => opt.Ignore())
                .ForMember(dest => dest.Seller, opt => opt.Ignore());

            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));

            CreateMap<MessageDto, Message>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Chat, opt => opt.Ignore())
                .ForMember(dest => dest.Sender, opt => opt.Ignore());

            CreateMap<PickupDetails, PickupDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

            CreateMap<PickupDetailsDto, PickupDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Transaction, opt => opt.Ignore());
            CreateMap<LoginRequestDto, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
            CreateMap<User, LoginDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.Password, opt => opt.Ignore()); 
            CreateMap<LoginDto, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 

        }
    }
}
