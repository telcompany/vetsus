using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Utilities;
using Vetsus.Domain.Entities;
using Vetsus.Domain.QueryParameters;
using Vetsus.Persistence.Contexts;

namespace Vetsus.Persistence.Repositories
{
    public sealed class VetRepository: GenericRepository<Vet>, IVetRepository
    {
        public VetRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }

        public async Task<PageList<VetResponse>> GetVetsByQueryAsync(VetQueryParameters queryParameters)
        {
            var records = (await GetAsync(queryParameters, "Id", "FirstName", "LastName", "Phone"))
                            .AsQueryable()
                            .Select(e => new VetResponse(e.Id, e.FirstName, e.LastName, e.Phone));

            if (!string.IsNullOrEmpty(queryParameters.Name))
                records = records.Where(e =>
                    e.FirstName.ToLowerInvariant().Contains(queryParameters.Name.ToLowerInvariant())
                    );

            if (!string.IsNullOrEmpty(queryParameters.SortBy))
                if (typeof(Owner).GetProperty(queryParameters.SortBy) != null)
                    records = records.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);


            var pagedRecords = PageList<VetResponse>.Create(records, queryParameters.PageNo, queryParameters.PageSize, 2000000);

            return pagedRecords;
        }
    }
}
