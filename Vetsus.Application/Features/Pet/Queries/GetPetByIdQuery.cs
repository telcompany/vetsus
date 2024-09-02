using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Wrappers;

namespace Vetsus.Application.Features.Pet.Queries
{
    public record GetPetByIdQuery(string Id) : IRequest<Response<PetResponse>>;

    public sealed class GetPetByIdQueryHandler : IRequestHandler<GetPetByIdQuery, Response<PetResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPetByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<PetResponse>> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
        {
            var requiredPet = await _unitOfWork.Pets.GetByIdAsync(request.Id);

            if (requiredPet == null)
                throw new NotFoundException($"Pet con id={request.Id} no registrado");

            var response = new PetResponse(requiredPet.Name,
                requiredPet.BirthDate,
                requiredPet.SpeciesId,
                requiredPet.OwnerId);

            return new Response<PetResponse>(response);
        }
    }
}
