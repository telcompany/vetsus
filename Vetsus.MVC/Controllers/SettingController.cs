using Microsoft.AspNetCore.Mvc;
using Vetsus.MVC.ViewModels;

namespace Vetsus.MVC.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult Company()
        {
            var indexVM = new IndexViewModel
            {
                BreadCrumb = new BreadCrumbViewModel
                {
                    Title = "Configuración",
                    Subtitle = "Empresa"
                }
            };

            return View(indexVM);
        }
    }
}
