using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models.Dto;
using System.Threading.Tasks;

namespace QuickBuy.WEB.ApplicationServices.Base
{
    public interface IPickupDetailsApplicationService
    {
        Task<IActionResult> CreatePickupDetails(PickupDetailsDto pickupDetailsDto);
        Task<IActionResult> GetPickupDetailsByTransactionId(int transactionId);
        Task<IActionResult> UpdatePickupDetails(PickupDetailsDto pickupDetailsDto);
    }
}
