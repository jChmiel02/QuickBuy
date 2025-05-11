using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using QuickBuy.UoW.Base;
using QuickBuy.WEB.ApplicationServices.Base;
using System;
using System.Threading.Tasks;

namespace QuickBuy.WEB.ApplicationServices
{
    public class TransactionApplicationService : ITransactionApplicationService
    {
        private readonly IManageTransactionsUoW _manageTransactionsUoW;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionApplicationService> _logger;

        public TransactionApplicationService(IManageTransactionsUoW manageTransactionsUoW, IMapper mapper, ILogger<TransactionApplicationService> logger)
        {
            _manageTransactionsUoW = manageTransactionsUoW;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> CreateTransaction(TransactionDto transactionDto)
        {
            try
            {
                var transaction = await _manageTransactionsUoW.CreateTransaction(transactionDto);
                return new OkObjectResult(_mapper.Map<TransactionDto>(transaction));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating transaction.");
                return new ObjectResult("An error occurred while creating the transaction.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetTransactionById(int id)
        {
            try
            {
                var transaction = await _manageTransactionsUoW.GetTransactionById(id);
                if (transaction == null)
                    return new NotFoundObjectResult("Transaction not found.");

                return new OkObjectResult(_mapper.Map<TransactionDto>(transaction));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving transaction with ID: {id}");
                return new ObjectResult("An error occurred while retrieving the transaction.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> UpdateTransactionStatus(int transactionId, TransactionStatus newStatus)
        {
            try
            {
                bool isUpdated = await _manageTransactionsUoW.UpdateTransactionStatus(transactionId, newStatus);
                return isUpdated
                    ? new OkObjectResult("Transaction status updated.")
                    : new NotFoundObjectResult("Transaction not found or update not allowed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating status for transaction {transactionId}.");
                return new ObjectResult("An error occurred while updating transaction status.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> ApproveTransactionBySeller(int transactionId)
        {
            try
            {
                bool isApproved = await _manageTransactionsUoW.ApproveTransactionBySeller(transactionId);
                return isApproved
                    ? new OkObjectResult("Transaction approved by seller.")
                    : new NotFoundObjectResult("Transaction not found or not in a valid state.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error approving transaction {transactionId} by seller.");
                return new ObjectResult("An error occurred while approving transaction.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> ConfirmTransactionByBuyer(int transactionId)
        {
            try
            {
                bool isConfirmed = await _manageTransactionsUoW.ConfirmTransactionByBuyer(transactionId);
                return isConfirmed
                    ? new OkObjectResult("Transaction confirmed by buyer.")
                    : new NotFoundObjectResult("Transaction not found or not in a valid state.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error confirming transaction {transactionId} by buyer.");
                return new ObjectResult("An error occurred while confirming transaction.") { StatusCode = 500 };
            }
        }
    }
}
