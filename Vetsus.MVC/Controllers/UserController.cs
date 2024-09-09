using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vetsus.Application.DTO;
using Vetsus.Application.Features.User.Commands;
using Vetsus.Application.Features.User.Queries;
using Vetsus.Application.Utilities;
using Vetsus.Domain.QueryParameters;

namespace Vetsus.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
		private readonly ISender _sender;

		public UserController(ISender sender)
		{
			_sender = sender;
		}

		public IActionResult Index()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == GlobalConstants.CustomClaims.UserId)?.Value;
			ViewBag.UserId = userId;

            return View();
        }

		[HttpGet]
		public async Task<IActionResult> GetAll(int offset, int limit)
		{
			var queryParameters = new UserQueryParameters() {
				PageNo = offset / limit + 1,
				PageSize = limit
			};
			var result = await _sender.Send(new GetUsersQuery(queryParameters));
			var users = result.Data;

			var response = new { rows = users?.Items, total = users?.TotalCount};

			return Json(response);
		}

        [HttpPost]
        public async Task<IActionResult> Add(RegisterUserRequest request)
        {
			var response = await _sender.Send(new RegisterUserCommand(request));

			return Json(response);
        }
    }
}
