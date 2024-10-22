using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Features.Owner.Queries;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Utilities;
using Vetsus.Application.Wrappers;
using Vetsus.Domain.QueryParameters;

namespace Vetsus.Application.Features.Species.Queries
{
    public record GetSpeciesQuery(SpeciesQueryParameters QueryParameters) : IRequest<Response<PageList<SpeciesResponse>>>;

    public sealed class GetSpeciesQueryHandler : IRequestHandler<GetSpeciesQuery, Response<PageList<SpeciesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetSpeciesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<PageList<SpeciesResponse>>> Handle(GetSpeciesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Species.GetSpeciesByQueryAsync(request.QueryParameters);

            return new Response<PageList<SpeciesResponse>>(result);
        }
    }
}
