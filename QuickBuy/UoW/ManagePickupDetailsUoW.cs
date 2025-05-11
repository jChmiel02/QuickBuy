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
    public class ManagePickupDetailsUoW : IManagePickupDetailsUoW
    {
        private readonly QuickBuyDbContext _context;
        private readonly IMapper _mapper;

        public ManagePickupDetailsUoW(QuickBuyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PickupDetails> CreatePickupDetails(PickupDetailsDto pickupDetailsDto)
        {
            var pickupDetails = _mapper.Map<PickupDetails>(pickupDetailsDto);
            try
            {
                _context.PickupDetails.Add(pickupDetails);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return pickupDetails;
        }

        public async Task<PickupDetails> GetPickupDetailsByTransactionId(int transactionId)
        {
            return await _context.PickupDetails
                .Include(pd => pd.Transaction)
                .FirstOrDefaultAsync(pd => pd.TransactionId == transactionId);
        }

        public async Task<bool> UpdatePickupDetails(PickupDetailsDto pickupDetailsDto)
        {
            var pickupDetails = await _context.PickupDetails.FirstOrDefaultAsync(pd => pd.Id == pickupDetailsDto.Id);
            if (pickupDetails == null)
            {
                return false;
            }

            pickupDetails.Location = pickupDetailsDto.Location;
            pickupDetails.ScheduledTime = pickupDetailsDto.ScheduledTime;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
