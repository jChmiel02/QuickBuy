using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using QuickBuy.WEB.ApplicationServices.Base;
using System.Threading.Tasks;

namespace QuickBuy.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionApplicationService _transactionApplicationService;

        public TransactionController(ITransactionApplicationService transactionApplicationService)
        {
            _transactionApplicationService = transactionApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDto transactionDto)
        {
            return await _transactionApplicationService.CreateTransaction(transactionDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionById([FromQuery] int id)
        {
            return await _transactionApplicationService.GetTransactionById(id);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTransactionStatus([FromBody] UpdateTransactionStatusDto updateDto)
        {
            if (updateDto == null || updateDto.TransactionId <= 0 || string.IsNullOrEmpty(updateDto.Status))
            {
                return BadRequest("Invalid data.");
            }

            if (!Enum.TryParse(updateDto.Status, true, out TransactionStatus newStatus))
            {
                return BadRequest("Invalid status value.");
            }

            return await _transactionApplicationService.UpdateTransactionStatus(updateDto.TransactionId, newStatus);
        }


        [HttpPatch]
        public async Task<IActionResult> ApproveTransactionBySeller([FromQuery] int transactionId)
        {
            return await _transactionApplicationService.ApproveTransactionBySeller(transactionId);
        }

        [HttpPatch]
        public async Task<IActionResult> ConfirmTransactionByBuyer([FromQuery] int transactionId)
        {
            return await _transactionApplicationService.ConfirmTransactionByBuyer(transactionId);
        }
    }

}
