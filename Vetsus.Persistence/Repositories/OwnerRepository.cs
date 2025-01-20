using Dapper;
using System.Data;
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

        public async Task<PageList<GetOwnerResponse>> GetOwnersByQueryAsync(OwnerQueryParameters queryParameters)
        {
            var parameters = new DynamicParameters();
            parameters.Add("pageNumber", queryParameters.PageNo, DbType.Int32, ParameterDirection.Input);
            parameters.Add("pageSize", queryParameters.PageSize, DbType.Int32, ParameterDirection.Input);

            using var connection = _dapperDataContext.Connection;

            var owners = await connection.QueryAsync<GetOwnerResponse>("spGetOwnerRecords", parameters, commandType: CommandType.StoredProcedure);

            if (!string.IsNullOrEmpty(queryParameters.Name))
                owners = owners.Where(e => e.FirstName.ToLowerInvariant().Contains(queryParameters.Name.ToLowerInvariant()));

            int totalCount = owners != null && owners.Any() ? owners.First().Total : 0;

            var pagedOwners = PageList<GetOwnerResponse>.Create(owners, queryParameters.PageNo, queryParameters.PageSize, totalCount);

            return pagedOwners;
        }
    }
}
