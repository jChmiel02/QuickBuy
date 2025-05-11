using System;
using System.ComponentModel.DataAnnotations;

namespace QuickBuy.Database.Models.Dto
{
    public class ChatDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ItemId is required.")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "BuyerId is required.")]
        public int BuyerId { get; set; }

        [Required(ErrorMessage = "SellerId is required.")]
        public int SellerId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }

    public class CreateChatDto
    {
        [Required(ErrorMessage = "ItemId is required.")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "BuyerId is required.")]
        public int BuyerId { get; set; }

        [Required(ErrorMessage = "SellerId is required.")]
        public int SellerId { get; set; }
    }
}
