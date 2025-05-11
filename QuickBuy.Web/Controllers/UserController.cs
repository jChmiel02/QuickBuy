using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models.Dto;
using QuickBuy.WEB.ApplicationServices.Base;
using System.Threading.Tasks;

namespace QuickBuy.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserApplicationService _userApplicationService;

        public UserController(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _userApplicationService.CreateUser(userDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            return await _userApplicationService.GetUserById(id);
        }

        [HttpPatch]
        public async Task<IActionResult> ChangePassword([FromBody] UserDto userDto, [FromQuery] string newPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                return BadRequest("New password cannot be empty.");
            }

            return await _userApplicationService.ChangePassword(userDto, newPassword);
        }
    }
}
