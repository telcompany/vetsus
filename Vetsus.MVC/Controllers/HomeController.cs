using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Vetsus.Application.DTO;
using Vetsus.Application.Features.User.Commands;
using Vetsus.Application.Features.User.Queries;
using Vetsus.MVC.Models;

namespace Vetsus.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISender _sender;

        public HomeController(ISender sender)
        {
            _sender = sender;
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
            string email = "user@vetsus.com";
            string password = "12345678";

            var result = await _sender.Send(new LoginUserQuery(email, password));

            return RedirectToAction("Privacy", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            await _sender.Send(new RegisterUserCommand(new RegisterUserRequest("UserTest", "user@vetsus.com", "12345678")));

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}