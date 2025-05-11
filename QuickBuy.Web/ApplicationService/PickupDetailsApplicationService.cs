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
    public class PickupDetailsApplicationService : IPickupDetailsApplicationService
    {
        private readonly IManagePickupDetailsUoW _managePickupDetailsUoW;
        private readonly IMapper _mapper;
        private readonly ILogger<PickupDetailsApplicationService> _logger;

        public PickupDetailsApplicationService(
            IManagePickupDetailsUoW managePickupDetailsUoW,
            IMapper mapper,
            ILogger<PickupDetailsApplicationService> logger)
        {
            _managePickupDetailsUoW = managePickupDetailsUoW;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> CreatePickupDetails(PickupDetailsDto pickupDetailsDto)
        {
            try
            {
                var pickupDetails = await _managePickupDetailsUoW.CreatePickupDetails(pickupDetailsDto);
                var resultDto = _mapper.Map<PickupDetailsDto>(pickupDetails);
                return new OkObjectResult(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating pickup details.");
                return new ObjectResult("An error occurred while creating pickup details.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetPickupDetailsByTransactionId(int transactionId)
        {
            try
            {
                var pickupDetails = await _managePickupDetailsUoW.GetPickupDetailsByTransactionId(transactionId);
                if (pickupDetails == null)
                {
                    return new NotFoundObjectResult("Pickup details not found.");
                }

                return new OkObjectResult(_mapper.Map<PickupDetailsDto>(pickupDetails));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching pickup details for transactionId: {transactionId}");
                return new ObjectResult("An error occurred while retrieving pickup details.") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> UpdatePickupDetails(PickupDetailsDto pickupDetailsDto)
        {
            try
            {
                bool updated = await _managePickupDetailsUoW.UpdatePickupDetails(pickupDetailsDto);
                if (updated)
                {
                    return new OkObjectResult("Pickup details updated successfully.");
                }

                return new NotFoundObjectResult("Pickup details not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating pickup details with ID: {pickupDetailsDto.Id}");
                return new ObjectResult("An error occurred while updating pickup details.") { StatusCode = 500 };
            }
        }
    }
}
