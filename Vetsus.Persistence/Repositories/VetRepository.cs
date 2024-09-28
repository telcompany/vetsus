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
    public sealed class VetRepository: GenericRepository<Vet>, IVetRepository
    {
        public VetRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }

        public async Task<PageList<VetResponse>> GetVetsByQueryAsync(VetQueryParameters queryParameters)
        {
            var parameters = new DynamicParameters();
            parameters.Add("pageNumber", queryParameters.PageNo, DbType.Int32, ParameterDirection.Input);
            parameters.Add("pageSize", queryParameters.PageSize, DbType.Int32, ParameterDirection.Input);

            using var connection = _dapperDataContext.Connection;

            var records = await connection.QueryAsync<VetResponse>("spGetVetRecords", parameters, commandType: CommandType.StoredProcedure);

            if (!string.IsNullOrEmpty(queryParameters.Name))
                records = records.Where(e => e.FirstName.ToLowerInvariant().Contains(queryParameters.Name.ToLowerInvariant()));

            var pagedRecords = PageList<VetResponse>.Create(records, queryParameters.PageNo, queryParameters.PageSize, records != null && records.Any() ? records.First().Total : 0);

            return pagedRecords;
        }
    }
}
