using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vetsus.MVC.Controllers
{
	[Authorize]
	public class OwnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
