using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Utilities;
using Vetsus.Domain.Entities;
using Vetsus.Domain.QueryParameters;
using Vetsus.Persistence.Contexts;

namespace Vetsus.Persistence.Repositories
{
    public sealed class SpeciesRepository: GenericRepository<Species>, ISpeciesRepository
    {
        public SpeciesRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }

        public async Task<PageList<SpeciesResponse>> GetSpeciesByQueryAsync(SpeciesQueryParameters queryParameters)
        {
            var records = (await GetAsync(queryParameters, "Id", "Name", "Total"))
                            .AsQueryable()
                            .Select(e => new SpeciesResponse(e.Id, e.Name, e.Total));

            int totalCount = records != null && records.Any() ? records.First().Total : 0;

            var pagedRecords = PageList<SpeciesResponse>.Create(records, queryParameters.PageNo, queryParameters.PageSize, totalCount);

            return pagedRecords;
        }
    }
}
