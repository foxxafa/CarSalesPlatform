using Application.Features.Commands.AppUserCommands.CreateUserCommand;
using Application.Features.Commands.AppUserCommands.LoginUser;
using Application.Results;
using CarSalesPlatformMVC.Areas.Website.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    public class AccountController : Controller
    {
        readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            var response = await _mediator.Send(loginUserCommandRequest);

            if (response.IsSuccess)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = false,
                    Secure = false,
                    Expires = response.Data.Item1.Expiration
                };

                HttpContext.Response.Cookies.Append("UserToken", response.Data.Item1.AccessToken, cookieOptions);
                HttpContext.Response.Cookies.Append("UserName", response.Data.Item2, cookieOptions);
            }

            return Json(new { isSuccess = response.IsSuccess, message = response.Message });
        }



        [HttpPost("[controller]/[action]")]
        [ValidateModel]
        public async Task<IActionResult> Create(CreateUserCommandRequest createUserCommandRequest)
        {

            Result response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);

        }


        [HttpGet]
        public IActionResult Logout()
        {
            // Cookie'leri sil
            Response.Cookies.Delete("UserToken");
            Response.Cookies.Delete("UserName");

            // Anasayfaya yönlendirme
            return RedirectToAction("Index", "Home", new { area = "Website" });
        }
    }
}
