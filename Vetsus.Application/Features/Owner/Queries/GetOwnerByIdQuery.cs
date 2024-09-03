using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Wrappers;

namespace Vetsus.Application.Features.Owner.Queries
{
    public record GetOwnerByIdQuery(string Id) : IRequest<Response<OwnerResponse>>;

    public sealed class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, Response<OwnerResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetOwnerByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<OwnerResponse>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var requiredOwner = await _unitOfWork.Owners.GetByIdAsync(request.Id);

            if (requiredOwner == null)
                throw new NotFoundException($"Owner con id={request.Id} no registrado");

            var response = new OwnerResponse(requiredOwner.FirstName, 
                requiredOwner.LastName, 
                requiredOwner.Address, 
                requiredOwner.Phone,
                requiredOwner.Email);

            return new Response<OwnerResponse>(response);
        }
    }
}
