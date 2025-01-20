using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Wrappers;

namespace Vetsus.Application.Features.Pet.Queries
{
    public record GetPetsByOwnerIdQuery(string OwnerId) : IRequest<Response<IEnumerable<GetPetsByOwnerIdResponse>>>;

    public sealed class GetPetsByOwnerIdQueryHandler : IRequestHandler<GetPetsByOwnerIdQuery, Response<IEnumerable<GetPetsByOwnerIdResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPetsByOwnerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<GetPetsByOwnerIdResponse>>> Handle(GetPetsByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            var pets = await _unitOfWork.Pets.GetPetsByOwnerId(request.OwnerId);

            return new Response<IEnumerable<GetPetsByOwnerIdResponse>>(pets);
        }
    }
}
