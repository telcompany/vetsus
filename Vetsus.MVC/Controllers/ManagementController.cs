using Microsoft.AspNetCore.Mvc;

namespace Vetsus.MVC.Controllers
{
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
