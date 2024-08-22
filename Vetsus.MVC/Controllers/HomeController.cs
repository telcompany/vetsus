using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.MVC.Models;

namespace Vetsus.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOrWOrk;

        public HomeController(IUnitOfWork unitOrWOrk)
        {
            _unitOrWOrk = unitOrWOrk;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Test()
        {
            var customers = await _unitOrWOrk.Customers.GetAsync(new Domain.Utilities.QueryParameters());

            return View(customers);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}