using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vetsus.Application.DTO;
using Vetsus.Application.Features.Owner.Commands;
using Vetsus.Application.Features.Owner.Queries;
using Vetsus.Application.Features.Pet.Commands;
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
                    Title = "Consultorio",
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

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _sender.Send(new GetOwnerByIdQuery(id));

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateOwnerAndPetRequest request)
        {
            var ownerResponse = await _sender.Send(new AddOwnerCommand(request.OwnerRequest));
            string ownerId = ownerResponse.Data;

            request.PetRequest.OwnerId = ownerId;

            await _sender.Send(new AddPetCommand(request.PetRequest));

            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateOwnerRequest request)
        {
            var response = await _sender.Send(new UpdateOwnerCommand(request));

            return Json(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _sender.Send(new DeleteOwnerCommand(id));

            return Json(null);
        }
    }
}
