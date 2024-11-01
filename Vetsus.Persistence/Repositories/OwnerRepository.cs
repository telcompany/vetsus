using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Utilities;
using Vetsus.Domain.Entities;
using Vetsus.Domain.QueryParameters;
using Vetsus.Persistence.Contexts;

namespace Vetsus.Persistence.Repositories
{
    public sealed class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }

        public async Task<PageList<OwnerResponse>> GetOwnersByQueryAsync(OwnerQueryParameters queryParameters)
        {
            var owners = (await GetAsync(queryParameters, "Id", "FirstName", "LastName", "Address", "Phone", "Email"))
                            .AsQueryable()
                            .Select(e => new OwnerResponse(e.FirstName, e.LastName, e.Address, e.Phone, e.Email, e.Total));

            if (!string.IsNullOrEmpty(queryParameters.Name))
                owners = owners.Where(e => 
                    e.FirstName.ToLowerInvariant().Contains(queryParameters.Name.ToLowerInvariant())
                    );

            if (!string.IsNullOrEmpty(queryParameters.SortBy))
                if (typeof(Owner).GetProperty(queryParameters.SortBy) != null)
                    owners = owners.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);

            int totalCount = owners != null && owners.Any() ? owners.First().Total : 0;

            var pagedOwners = PageList<OwnerResponse>.Create(owners, queryParameters.PageNo, queryParameters.PageSize, totalCount);

            return pagedOwners;
        }
    }
}
