using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Wrappers;

namespace Vetsus.Application.Features.Vet.Queries
{
    public record GetVetByIdQuery(string Id) : IRequest<Response<VetByIdResponse>>;

    public sealed class GetVetByIdQueryHandler : IRequestHandler<GetVetByIdQuery, Response<VetByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetVetByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<VetByIdResponse>> Handle(GetVetByIdQuery request, CancellationToken cancellationToken)
        {
            var requiredRecord = await _unitOfWork.Vets.GetByIdAsync(request.Id);

            if (requiredRecord == null)
                throw new NotFoundException($"Vet con id={request.Id} no registrado");

            var response = new VetByIdResponse(requiredRecord.Id, requiredRecord.FirstName, requiredRecord.LastName, requiredRecord.Phone);

            return new Response<VetByIdResponse>(response);
        }
    }
}
