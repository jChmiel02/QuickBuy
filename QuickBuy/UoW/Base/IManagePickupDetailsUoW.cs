using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using System.Threading.Tasks;

namespace QuickBuy.UoW.Base
{
    public interface IManagePickupDetailsUoW
    {
        Task<PickupDetails> CreatePickupDetails(PickupDetailsDto pickupDetailsDto);
        Task<PickupDetails> GetPickupDetailsByTransactionId(int transactionId);
        Task<bool> UpdatePickupDetails(PickupDetailsDto pickupDetailsDto);
    }
}
