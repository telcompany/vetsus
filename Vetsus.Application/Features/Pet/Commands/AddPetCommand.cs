using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Wrappers;

namespace Vetsus.Application.Features.Pet.Commands
{
    public record AddPetCommand(CreatePetRequest CreatePetRequest) : IRequest<Response<string>>;

    public sealed class AddPetCommandHandler : IRequestHandler<AddPetCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddPetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<string>> Handle(AddPetCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            var petId = await _unitOfWork.Pets.AddAsync(new Domain.Entities.Pet
            {
                Name = request.CreatePetRequest.Name,
                BirthDate = request.CreatePetRequest.BirthDate,
                SpeciesId = request.CreatePetRequest.SpeciesId,
                OwnerId = request.CreatePetRequest.OwnerId
            });
            _unitOfWork.CommitAndCloseConnection();

            return new Response<string>(petId);
        }
    }
}
