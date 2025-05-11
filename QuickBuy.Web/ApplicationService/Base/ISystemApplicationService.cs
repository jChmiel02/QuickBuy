using Microsoft.AspNetCore.Mvc;

namespace QuickBuy.Web.ApplicationService.Base
{
    public interface ISystemApplicationService
    {
        Task<IActionResult> Ping();
    }
}
