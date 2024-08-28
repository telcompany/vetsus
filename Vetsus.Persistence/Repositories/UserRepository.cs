using Dapper;
using System.Data;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Domain.Entities;
using Vetsus.Persistence.Contexts;

namespace Vetsus.Persistence.Repositories
{
    public sealed class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return (await GetBySpecificColumnAsync("Email", email)).AsQueryable().FirstOrDefault();
        }

        public async Task<IEnumerable<Permission>> GetUserPermissions(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", user.Id, DbType.String, ParameterDirection.Input, size: 22);

            using var connection = _dapperDataContext.Connection;

            return await connection.QueryAsync<Permission>("spGetUserPermissions", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
