using CleanArchitecture.Application.Ecommerce.DTOs.Ecommerce.UserModule.User;
using CleanArchitecture.Application.Ecommerce.IServices.Users;
using CleanArchitecture.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers.Ecommerce.UserModule.User
{
    [Area("UserModule")]
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
            var request = new DataTableRequest
            {
                Draw = Convert.ToInt32(Request.Query["draw"]),
                Start = Convert.ToInt32(Request.Query["start"]),
                Length = Convert.ToInt32(Request.Query["length"])
            };
            var result = await _userService.GetUserData(request);
            return Ok(result);
        }

        [HttpGet("create-user-modal")]
        public async Task<IActionResult> CreateUserModal()
        {
            var response = await _userService.HandleCreateUserModalLogic(); 
            return View("~/Views/Ecommerce/UserModule/User/Modals/Create.cshtml", response.Values);
        }

        [HttpPost("create-user")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        k => k.Key,
                        v => v.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return StatusCode(422, new
                {
                    message = "Validation failed",
                    data = errors
                });
            }

            var result = await _userService.CreateUser(request);
            return Ok(result);
        }

        [HttpGet("edit-user-modal")]
        public async Task<IActionResult> EditUserModal(int id)
        {
            var response = await _userService.HandleEditUserModalLogic(id);

            if (response.Status)
            {
                return View("~/Views/Ecommerce/UserModule/User/Modals/Edit.cshtml", response.Values);
            }
            else
            {
                ViewBag.Message = response.Message;
                return View("~/Views/Shared/Errors/Modals/404.cshtml", response.Message);
            }
        }

        [HttpPost("edit-user")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserRequest request, int id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        k => k.Key,
                        v => v.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return StatusCode(422, new
                {
                    message = "Validation failed",
                    data = errors
                });
            }

            var result = await _userService.EditUser(request, id);
            return Ok(result);
        }
    }
}
