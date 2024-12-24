using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Utilities;
using Vetsus.Application.Wrappers;
using Vetsus.Domain.QueryParameters;

namespace Vetsus.Application.Features.Owner.Queries
{
    public record GetOwnersQuery(OwnerQueryParameters QueryParameters) : IRequest<Response<PageList<GetOwnerResponse>>>;

    public sealed class GetOwnersQueryHandler : IRequestHandler<GetOwnersQuery, Response<PageList<GetOwnerResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetOwnersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<PageList<GetOwnerResponse>>> Handle(GetOwnersQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Owners.GetOwnersByQueryAsync(request.QueryParameters);

            return new Response<PageList<GetOwnerResponse>>(result);
        }
    }
}
