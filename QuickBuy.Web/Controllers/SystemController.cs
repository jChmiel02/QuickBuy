using Microsoft.AspNetCore.Mvc;
using QuickBuy.Web.ApplicationService.Base;

namespace QuickBuy.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemController : Controller
    {
        private readonly ISystemApplicationService _systemAplicationService;
        public SystemController(ISystemApplicationService systemAplicationService)
        {
            _systemAplicationService = systemAplicationService;
        }
        [HttpGet]
        public async Task<IActionResult> Ping()
        {
            return await _systemAplicationService.Ping();
        }
    }
}
