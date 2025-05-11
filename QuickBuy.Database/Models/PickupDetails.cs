using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBuy.Database.Models
{
    public class PickupDetails
    {
        public int Id { get; set; }

        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }

        public string Location { get; set; }
        public DateTime ScheduledTime { get; set; }
    }

}
