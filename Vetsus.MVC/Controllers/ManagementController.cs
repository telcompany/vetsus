using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vetsus.MVC.Controllers
{
    [Authorize]
    public class ManagementController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Owners()
        {
            return View();
        }
    }
}
