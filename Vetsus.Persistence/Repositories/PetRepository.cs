using Dapper;
using System.Data;
using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Domain.Entities;
using Vetsus.Persistence.Contexts;
using static Vetsus.Domain.Errors.Errors;

namespace Vetsus.Persistence.Repositories
{
    public sealed class PetRepository: GenericRepository<Pet>, IPetRepository
    {
        public PetRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }

        public async Task<IEnumerable<GetPetsByOwnerIdResponse>> GetPetsByOwnerId(string ownerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ownerId", ownerId, DbType.String, ParameterDirection.Input, size: 22);

            using var connection = _dapperDataContext.Connection;

            return await connection.QueryAsync<GetPetsByOwnerIdResponse>("spGetPetsByOwnerId", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
