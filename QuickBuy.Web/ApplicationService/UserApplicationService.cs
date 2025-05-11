using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using QuickBuy.UoW.Base;
using QuickBuy.WEB.ApplicationServices.Base;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuickBuy.WEB.ApplicationServices
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly IManageUsersUoW _manageUsersUoW;
        private readonly IMapper _mapper;

        public UserApplicationService(IManageUsersUoW manageUsersUoW, IMapper mapper)
        {
            _manageUsersUoW = manageUsersUoW;
            _mapper = mapper;
        }

        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            if (!IsValidEmail(userDto.Email))
            {
                return new BadRequestObjectResult("Invalid email format.");
            }

            bool userExist = await _manageUsersUoW.CheckIfUserExist(userDto);
            if (userExist)
            {
                return new BadRequestObjectResult("User with this email or name already exists.");
            }

            var createdUser = await _manageUsersUoW.CreateUser(userDto);
            var resultDto = _mapper.Map<UserDto>(createdUser);
            return new OkObjectResult(resultDto);
        }

        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _manageUsersUoW.GetUserById(id);
            if (user == null)
            {
                return new NotFoundObjectResult("User not found.");
            }

            return new OkObjectResult(_mapper.Map<UserDto>(user));
        }

        public async Task<IActionResult> ChangePassword(UserDto userDto, string newPassword)
        {
            bool userExist = await _manageUsersUoW.CheckIfUserExist(userDto);
            if (!userExist)
            {
                return new NotFoundObjectResult("User with this email or name does not exist.");
            }

            bool isChanged = await _manageUsersUoW.ChangePassword(userDto, newPassword);
            if (isChanged)
            {
                return new OkObjectResult("Password changed successfully.");
            }

            return new BadRequestObjectResult("Invalid username or password. Please try again.");
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            return emailRegex.IsMatch(email);
        }
    }
}
