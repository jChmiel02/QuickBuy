using AutoMapper;
using QuickBuy.Database.DbContext;
using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using QuickBuy.UoW.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace QuickBuy.UoW
{
    public class ManageTransactionsUoW : IManageTransactionsUoW
    {
        private readonly QuickBuyDbContext _context;
        private readonly IMapper _mapper;

        public ManageTransactionsUoW(QuickBuyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Transaction> CreateTransaction(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            try
            {
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return transaction;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _context.Transactions
                .Include(t => t.Item)
                .Include(t => t.Buyer)
                .Include(t => t.Seller)
                .Include(t => t.PickupDetails)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> UpdateTransactionStatus(int transactionId, TransactionStatus newStatus)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
            if (transaction == null)
            {
                return false;
            }

            transaction.Status = newStatus;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApproveTransactionBySeller(int transactionId)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
            if (transaction == null || transaction.Status != TransactionStatus.Pending)
            {
                return false;
            }

            transaction.Status = TransactionStatus.AwaitingBuyer;
            transaction.SellerApprovedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConfirmTransactionByBuyer(int transactionId)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
            if (transaction == null || transaction.Status != TransactionStatus.AwaitingBuyer)
            {
                return false;
            }

            transaction.Status = TransactionStatus.Approved;
            transaction.BuyerConfirmedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
