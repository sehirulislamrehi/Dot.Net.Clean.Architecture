using CleanArchitecture.Application.Ecommerce.IServices.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers.Ecommerce.UserModule.User
{

    [Authorize]
    [Route("admin/user-module/user")]
    public class UserController : Controller
    {

        private IUserService _userService;
        public UserController(
            IUserService userService
        ) { 
            _userService = userService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("~/Views/Ecommerce/UserModule/User/Index.cshtml");
        }

        [HttpGet("data")]
        public async Task<IActionResult> Data()
        {
            var result = await _userService.GetUserData();
            return Ok(result);
        }

        [HttpGet("create-user-modal")]
        public async Task<IActionResult> CreateUserModal()
        {
            return View("~/Views/Ecommerce/UserModule/User/Modals/Create.cshtml");
        }
    }
}
