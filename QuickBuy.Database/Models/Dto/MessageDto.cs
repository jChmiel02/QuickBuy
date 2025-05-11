using System;
using System.ComponentModel.DataAnnotations;

namespace QuickBuy.Database.Models.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Chat ID is required.")]
        public int ChatId { get; set; }

        [Required(ErrorMessage = "Sender ID is required.")]
        public int SenderId { get; set; }

        [Required(ErrorMessage = "Message content is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Sent time is required.")]
        public DateTime SentAt { get; set; }
    }

    public class SendMessageDto
    {
        [Required(ErrorMessage = "Chat ID is required.")]
        public int ChatId { get; set; }

        [Required(ErrorMessage = "Sender ID is required.")]
        public int SenderId { get; set; }

        [Required(ErrorMessage = "Message content is required.")]
        public string Content { get; set; }
    }
}
