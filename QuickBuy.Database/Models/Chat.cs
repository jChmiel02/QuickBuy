using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBuy.Database.Models
{
        public class Chat
        {
            public int Id { get; set; }
            public int ItemId { get; set; }
            public Item Item { get; set; }

            public int BuyerId { get; set; }
            public User Buyer { get; set; }

            public int SellerId { get; set; }
            public User Seller { get; set; }

            public DateTime CreatedAt { get; set; }

            public ICollection<Message> Messages { get; set; }
        }
    }
