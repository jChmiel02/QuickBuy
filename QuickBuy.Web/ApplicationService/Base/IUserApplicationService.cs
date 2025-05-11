using QuickBuy.Database.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace QuickBuy.WEB.ApplicationServices.Base
{
    public interface IUserApplicationService
    {
        Task<IActionResult> CreateUser(UserDto userDto);
        Task<IActionResult> GetUserById(int id);
        Task<IActionResult> ChangePassword(UserDto userDto, string newPassword);
    }
}
