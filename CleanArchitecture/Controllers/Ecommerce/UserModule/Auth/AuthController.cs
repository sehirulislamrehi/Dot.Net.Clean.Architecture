using CleanArchitecture.Application.Ecommerce.IServices.Users;
using CleanArchitecture.Domain.Ecommerce.DTOs.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.Controllers.Ecommerce.UserModule.Auth
{
    [AllowAnonymous]
    [Route("auth")]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        public AuthController(
            IAuthService authService    
        )
        {
            _authService = authService;
        }

        [HttpGet("/")]
        public IActionResult LoginPage()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            return View("~/Views/Ecommerce/UserModule/Auth/Login.cshtml");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Ecommerce/UserModule/Auth/Login.cshtml", loginRequestDTO);
            }

            var result = await _authService.HandleLogin(loginRequestDTO, HttpContext);

            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View("~/Views/Ecommerce/UserModule/Auth/Login.cshtml", loginRequestDTO);
            }

            // Login success → redirect to dashboard
            return RedirectToAction("Dashboard", "Dashboard");
        }

        [Authorize]
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
