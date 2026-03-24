using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers.Ecommerce.Dashboard
{
    [Authorize]
    [Route("[controller]")]
    public class DashboardController : Controller
    {

        [HttpGet("/Admin/Dashboard")]
        public IActionResult Dashboard()
        {
            return View("~/Views/Ecommerce/Dashboard/Index.cshtml");
        }
    }
}
