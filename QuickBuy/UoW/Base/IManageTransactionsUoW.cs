using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using System.Threading.Tasks;

namespace QuickBuy.UoW.Base
{
    public interface IManageTransactionsUoW
    {
        Task<Transaction> CreateTransaction(TransactionDto transactionDto);
        Task<Transaction> GetTransactionById(int id);
        Task<bool> UpdateTransactionStatus(int transactionId, TransactionStatus newStatus);
        Task<bool> ApproveTransactionBySeller(int transactionId);
        Task<bool> ConfirmTransactionByBuyer(int transactionId);
    }
}
