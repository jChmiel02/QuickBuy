using Microsoft.AspNetCore.Mvc;
using QuickBuy.Web.ApplicationService.Base;

namespace QuickBuy.Web.ApplicationService
{
    public class SystemApplicationService : ISystemApplicationService
    {
        public async Task<IActionResult> Ping()
        {
            return new OkObjectResult("Service is available");
        }
    }
}
