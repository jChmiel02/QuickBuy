using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using System.Threading.Tasks;

namespace QuickBuy.UoW.Base
{
    public interface IManageUsersUoW
    {
        Task<User> CreateUser(UserDto userDto);
        Task<User> GetUserById(int id);
        Task<bool> CheckIfUserExist(UserDto userDto);
        Task<bool> ChangePassword(UserDto userDto, string newPassword);
        Task<User> CheckIfUserPasswordCorrect(UserDto userDto);
    }
}
