using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vetsus.Application.DTO;
using Vetsus.Application.Features.User.Commands;
using Vetsus.Application.Features.User.Queries;
using Vetsus.Domain.QueryParameters;
using Vetsus.MVC.ViewModels;

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
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
			ViewBag.UserId = userId;

            var indexVM = new IndexViewModel
            {
                BreadCrumb = new BreadCrumbViewModel
                {
                    Title = "Configuración",
                    Subtitle = "Usuarios"
                },
                PageTitle = "Listado de usuarios"
            };

            return View(indexVM);
        }

        public IActionResult Profile()
        {
            var indexVM = new IndexViewModel
            {
                BreadCrumb = new BreadCrumbViewModel
                {
                    Title = "Configuración",
                    Subtitle = "Perfil"
                },
                PageTitle = ""
            };

            return View(indexVM);
        }

        #region API_CALLS

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

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _sender.Send(new GetUserByIdQuery(id));

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterUserRequest request)
        {
            var response = await _sender.Send(new RegisterUserCommand(request));

			return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            var response = await _sender.Send(new UpdateUserCommand(request));

            return Json(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _sender.Send(new DeleteUserCommand(id));

            return Json(null);
        }

        #endregion
    }
}
