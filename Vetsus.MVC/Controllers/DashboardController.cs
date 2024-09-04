using Microsoft.AspNetCore.Mvc;

namespace Vetsus.MVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
