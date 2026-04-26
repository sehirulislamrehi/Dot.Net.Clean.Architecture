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
                Length = Convert.ToInt32(Request.Query["length"])
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
    }
}
