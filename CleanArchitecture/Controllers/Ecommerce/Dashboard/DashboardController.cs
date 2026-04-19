using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers.Ecommerce.Dashboard
{
    [Authorize]
    [Route("admin/dashboard")]
    public class DashboardController : Controller
    {

        [HttpGet("")]
        public IActionResult Dashboard()
        {
            return View("~/Views/Ecommerce/Dashboard/Index.cshtml");
        }
    }
}
