using Microsoft.AspNetCore.Mvc;
using QuickBuy.Database.Models.Dto;

namespace QuickBuy.Web.ApplicationService.Base
{
    public interface ILoginApplicationService
    {
        Task<IActionResult> Login(LoginRequestDto loginRequest);
    }
}
