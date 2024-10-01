using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vetsus.Application.Features.Owner.Queries;
using Vetsus.Application.Features.User.Queries;
using Vetsus.Domain.QueryParameters;
using Vetsus.MVC.ViewModels;

namespace Vetsus.MVC.Controllers
{
	[Authorize]
	public class OwnerController : Controller
    {
        private readonly ISender _sender;

        public OwnerController(ISender sender)
        {
            _sender = sender;
        }

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

        [HttpGet]
        public async Task<IActionResult> GetAll(int offset, int limit)
        {
            var queryParameters = new OwnerQueryParameters()
            {
                PageNo = offset / limit + 1,
                PageSize = limit
            };

            var result = await _sender.Send(new GetOwnersQuery(queryParameters));
            var users = result.Data;

            var response = new { rows = users?.Items, total = users?.TotalCount };

            return Json(response);
        }
    }
}
