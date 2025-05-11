using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBuy.Database.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int SellerId { get; set; }
        public User Seller { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsSold { get; set; }

        public ICollection<Chat> Chats { get; set; }
        public Transaction Transaction { get; set; }
    }
}
