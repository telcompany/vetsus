using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.MVC.Models;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            string userName = "Admin";
            string password = "password";

            if (userName == "Admin" && password == "password")
            {
                // Create the identity for the user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);
                
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Privacy", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
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