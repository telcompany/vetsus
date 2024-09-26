using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vetsus.Application.DTO;
using Vetsus.Application.Features.User.Commands;
using Vetsus.Application.Features.Vet.Command;
using Vetsus.Application.Features.Vet.Queries;
using Vetsus.Domain.QueryParameters;
using Vetsus.MVC.ViewModels;

namespace Vetsus.MVC.Controllers
{
    [Authorize]
	public class VetController : Controller
    {
        private readonly ISender _sender;

        public VetController(ISender sender)
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
                    Subtitle = "Doctores"
                },
                PageTitle = "Listado de doctores"
            };

            return View(indexVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(string id)
        {
            var vetVM = new VetViewModel();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var response = await _sender.Send(new GetVetByIdQuery(id));
                vetVM.Id = id;
                vetVM.Firstname = response.Data.FirstName;
                vetVM.Lastname = response.Data.LastName;
                vetVM.Phone = response.Data.Phone;
            }

            return PartialView("_VetModalPartial", vetVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int offset, int limit)
        {
            var queryParameters = new VetQueryParameters()
            {
                PageNo = offset / limit + 1,
                PageSize = limit
            };
            var result = await _sender.Send(new GetVetsQuery(queryParameters));
            var records = result.Data;

            var response = new { rows = records?.Items, total = records?.TotalCount };

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateVetRequest request)
        {
            var response = await _sender.Send(new AddVetCommand(request));

            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateVetRequest request)
        {
            var response = await _sender.Send(new UpdateVetCommand(request));

            return Json(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _sender.Send(new DeleteVetCommand(id));

            return Json(null);
        }
    }
}
