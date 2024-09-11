using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vetsus.MVC.ViewModels;

namespace Vetsus.MVC.Controllers
{
	[Authorize]
	public class VetController : Controller
    {
        public IActionResult Index()
        {
            var indexVM = new IndexViewModel
            {
                BreadCrumb = new BreadCrumbViewModel
                {
                    Title = "Gestión",
                    Subtitle = "Doctores"
                },
                PageTitle = "Listado de doctores"
            };

            return View(indexVM);
        }
    }
}
