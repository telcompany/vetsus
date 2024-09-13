using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vetsus.MVC.ViewModels;

namespace Vetsus.MVC.Controllers
{
	[Authorize]
	public class OwnerController : Controller
    {
        public IActionResult Index()
        {
            var indexVM = new IndexViewModel
            {
                BreadCrumb = new BreadCrumbViewModel
                {
                    Title = "Gestión",
                    Subtitle = "Dueños"
                },
                PageTitle = "Listado de dueños"
            };

            return View(indexVM);
        }
    }
}
