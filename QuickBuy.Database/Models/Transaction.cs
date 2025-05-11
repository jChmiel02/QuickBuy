using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBuy.Database.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int BuyerId { get; set; }
        public User Buyer { get; set; }

        public int SellerId { get; set; }
        public User Seller { get; set; }

        public TransactionStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? SellerApprovedAt { get; set; }
        public DateTime? BuyerConfirmedAt { get; set; }

        public PickupDetails PickupDetails { get; set; }

        public string? RejectionReason { get; set; }
    }

    public enum TransactionStatus
    {
        Pending,        
        AwaitingBuyer,     
        BuyerRejected,     
        Approved,        
        Completed,         
        Cancelled
    }
}
