using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using System.Threading.Tasks;

namespace QuickBuy.WEB.ApplicationServices.Base
{
    public interface ITransactionApplicationService
    {
        Task<IActionResult> CreateTransaction(TransactionDto transactionDto);
        Task<IActionResult> GetTransactionById(int id);
        Task<IActionResult> UpdateTransactionStatus(int transactionId, TransactionStatus newStatus);
        Task<IActionResult> ApproveTransactionBySeller(int transactionId);
        Task<IActionResult> ConfirmTransactionByBuyer(int transactionId);
    }
}
