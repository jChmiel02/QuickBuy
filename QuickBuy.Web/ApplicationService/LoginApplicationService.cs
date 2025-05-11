using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models.Dto;
using QuickBuy.UoW.Base;
using QuickBuy.Web.ApplicationService.Base;
using System.Threading.Tasks;

namespace QuickBuy.Web.ApplicationService
{
    public class LoginApplicationService : ILoginApplicationService
    {
        private readonly IManageUsersUoW _manageUsersUoW;
        private readonly IMapper _mapper;

        public LoginApplicationService(IManageUsersUoW manageUsersUoW, IMapper mapper)
        {
            _manageUsersUoW = manageUsersUoW;
            _mapper = mapper;
        }

        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var userDto = _mapper.Map<UserDto>(loginRequest);

            bool userExists = await _manageUsersUoW.CheckIfUserExist(userDto);
            if (!userExists)
            {
                return new NotFoundObjectResult("User does not exist.");
            }

            var userRecord = await _manageUsersUoW.CheckIfUserPasswordCorrect(userDto);
            if (userRecord == null)
            {
                return new BadRequestObjectResult("Invalid password.");
            }

            var loginDto = _mapper.Map<LoginDto>(userRecord);
            loginDto.UserId = userRecord.Id;

            return new OkObjectResult(loginDto);
        }
    }
}
