using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models.Dto;
using QuickBuy.WEB.ApplicationServices.Base;
using System.Threading.Tasks;

namespace QuickBuy.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PickupDetailsController : Controller
    {
        private readonly IPickupDetailsApplicationService _pickupDetailsApplicationService;

        public PickupDetailsController(IPickupDetailsApplicationService pickupDetailsApplicationService)
        {
            _pickupDetailsApplicationService = pickupDetailsApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePickupDetails([FromBody] PickupDetailsDto pickupDetailsDto)
        {
            return await _pickupDetailsApplicationService.CreatePickupDetails(pickupDetailsDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetPickupDetailsByTransactionId([FromQuery] int transactionId)
        {
            return await _pickupDetailsApplicationService.GetPickupDetailsByTransactionId(transactionId);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePickupDetails([FromBody] PickupDetailsDto pickupDetailsDto)
        {
            return await _pickupDetailsApplicationService.UpdatePickupDetails(pickupDetailsDto);
        }
    }

}
