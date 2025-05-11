using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models.Dto;
using QuickBuy.Web.ApplicationService.Base;
using System.Threading.Tasks;

namespace QuickBuy.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly ILoginApplicationService _loginApplicationService;

        public AuthController(ILoginApplicationService loginApplicationService)
        {
            _loginApplicationService = loginApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _loginApplicationService.Login(loginRequest);
        }
    }
}
