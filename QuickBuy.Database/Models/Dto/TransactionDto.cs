using System;

namespace QuickBuy.Database.Models.Dto
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? SellerApprovedAt { get; set; }
        public DateTime? BuyerConfirmedAt { get; set; }
        public PickupDetailsDto? PickupDetails { get; set; }
        public string? RejectionReason { get; set; }
    }

    public class CreateTransactionDto
    {
        public int ItemId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
    }

    public class UpdateTransactionStatusDto
    {
        public int TransactionId { get; set; }
        public string Status { get; set; }
        public string? RejectionReason { get; set; }
    }
}
