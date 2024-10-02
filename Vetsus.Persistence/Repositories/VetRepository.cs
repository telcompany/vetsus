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
            parameters.Add("tableName", typeof(Vet).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
            parameters.Add("pageNumber", queryParameters.PageNo, DbType.Int32, ParameterDirection.Input);
            parameters.Add("pageSize", queryParameters.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("columns", "Id,FirstName,LastName,Phone,Total", DbType.String, ParameterDirection.Input);

            using var connection = _dapperDataContext.Connection;

            var records = await connection.QueryAsync<VetResponse>("spGetAllRecords", parameters, commandType: CommandType.StoredProcedure);

            if (!string.IsNullOrEmpty(queryParameters.Name))
                records = records.Where(e => e.FirstName.ToLowerInvariant().Contains(queryParameters.Name.ToLowerInvariant()));

            int totalCount = records != null && records.Any() ? records.First().Total : 0;

            var pagedRecords = PageList<VetResponse>.Create(records, queryParameters.PageNo, queryParameters.PageSize, totalCount);

            return pagedRecords;
        }
    }
}
