using CleanArchitecture.Application.Ecommerce.DTOs.Ecommerce.UserModule.Role;
using CleanArchitecture.Application.Ecommerce.DTOs.Ecommerce.UserModule.User;
using CleanArchitecture.Application.Ecommerce.IServices.Roles;
using CleanArchitecture.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers.Ecommerce.UserModule.Role
{
    [Area("UserModule")]
    [Authorize]
    [Route("admin/user-module/role")]
    public class RoleController : Controller
    {

        private IRoleService _roleService;
        public RoleController(IRoleService roleService) { 
            _roleService = roleService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("~/Views/Ecommerce/UserModule/Role/Index.cshtml");
        }

        [HttpGet("data")]
        public async Task<IActionResult> Data()
        {
            var request = new DataTableRequest
            {
                Draw = Convert.ToInt32(Request.Query["draw"]),
                Start = Convert.ToInt32(Request.Query["start"]),
                Length = Convert.ToInt32(Request.Query["length"]),
                Search = Request.Query["search[value]"]
            };
            var result = await _roleService.GetRoleData(request);
            return Ok(result);
        }
        
        [HttpGet("create-role-modal")]
        public async Task<IActionResult> CreateRoleModal()
        {
            var response = await _roleService.HandleCreateRoleModalLogic();
            return View("~/Views/Ecommerce/UserModule/Role/Modals/Create.cshtml", response.Values);
        }

        [HttpPost("create-role")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleRequest request)
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

            var result = await _roleService.CreateRole(request);
            return Ok(result);
        }

        [HttpGet("edit-role-modal")]
        public async Task<IActionResult> EditRoleModal(int id)
        {
            var response = await _roleService.HandleEditRoleModalLogic(id);

            if (response.Status)
            {
                return View("~/Views/Ecommerce/UserModule/Role/Modals/Edit.cshtml", response.Values);
            }
            else
            {
                ViewBag.Message = response.Message;
                return View("~/Views/Shared/Errors/Modals/404.cshtml", response.Message);
            }
        }


        [HttpPost("edit-role")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(CreateRoleRequest request, int id)
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

            var result = await _roleService.EditRole(request, id);
            return Ok(result);
        }
    }
}
