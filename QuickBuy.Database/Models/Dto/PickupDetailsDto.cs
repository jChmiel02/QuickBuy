using System;
using System.ComponentModel.DataAnnotations;

namespace QuickBuy.Database.Models.Dto
{
    public class PickupDetailsDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Transaction ID is required.")]
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Scheduled time is required.")]
        public DateTime ScheduledTime { get; set; }
    }

    public class CreatePickupDetailsDto
    {
        [Required(ErrorMessage = "Transaction ID is required.")]
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Scheduled time is required.")]
        public DateTime ScheduledTime { get; set; }
    }
}
