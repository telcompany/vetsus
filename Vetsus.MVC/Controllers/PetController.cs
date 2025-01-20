using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vetsus.Application.DTO;
using Vetsus.Application.Features.Pet.Commands;

namespace Vetsus.MVC.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
        private readonly ISender _sender;

        public PetController(ISender sender)
        {
            _sender = sender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreatePetRequest request)
        {
            await _sender.Send(new AddPetCommand(request));

            return Json(null);
        }
    }
}
