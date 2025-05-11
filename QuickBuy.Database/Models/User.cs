using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBuy.Database.Models

{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Item> ItemsForSale { get; set; }
        public ICollection<Transaction> TransactionsAsBuyer { get; set; }
        public ICollection<Transaction> TransactionsAsSeller { get; set; }
        public ICollection<Chat> ChatsAsBuyer { get; set; }
        public ICollection<Chat> ChatsAsSeller { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}