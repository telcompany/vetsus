using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Utilities;
using Vetsus.Application.Wrappers;
using Vetsus.Domain.QueryParameters;

namespace Vetsus.Application.Features.Vet.Queries
{
    public record GetVetsQuery(VetQueryParameters QueryParameters) : IRequest<Response<PageList<VetResponse>>>;

    public sealed class GetVetsQueryHandler : IRequestHandler<GetVetsQuery, Response<PageList<VetResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetVetsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<PageList<VetResponse>>> Handle(GetVetsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Vets.GetVetsByQueryAsync(request.QueryParameters);

            return new Response<PageList<VetResponse>>(result);
        }
    }
}
