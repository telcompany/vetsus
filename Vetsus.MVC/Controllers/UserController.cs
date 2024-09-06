using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vetsus.Application.Features.User.Queries;
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
            return View();
        }

		[HttpGet]
		public async Task<IActionResult> GetAll(int offset, int limit)
		{
			var queryParameters = new UserQueryParameters() {
				PageNo = offset + 1,
				PageSize = limit
			};
			var result = await _sender.Send(new GetUsersQuery(queryParameters));
			var users = result.Data;

			var response = new { rows = users?.Items, total = users?.TotalCount};

			return Json(response);
		}
	}
}
