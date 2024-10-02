using Dapper;
using System;
using System.Data;
using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Utilities;
using Vetsus.Domain.Entities;
using Vetsus.Domain.QueryParameters;
using Vetsus.Persistence.Contexts;
using static Dapper.SqlMapper;

namespace Vetsus.Persistence.Repositories
{
    public sealed class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }

        public async Task AddUserRole(string roleId, string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId, DbType.String, ParameterDirection.Input, size: 22);
            parameters.Add("roleId", roleId, DbType.String, ParameterDirection.Input, size: 22);

            await _dapperDataContext.Connection.ExecuteAsync("spInsertUserRole", 
                parameters, 
                _dapperDataContext.Transaction, 
                commandType: CommandType.StoredProcedure);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return (await GetBySpecificColumnAsync("Email", email)).AsQueryable().FirstOrDefault();
        }

        public async Task<UserByIdResponse> GetUserByIdQueryAsync(string id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.String, ParameterDirection.Input, size: 22);

            using var connection = _dapperDataContext.Connection;

            return await connection.QuerySingleOrDefaultAsync<UserByIdResponse>("spGetUserById", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Permission>> GetUserPermissions(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", user.Id, DbType.String, ParameterDirection.Input, size: 22);

            using var connection = _dapperDataContext.Connection;

            return await connection.QueryAsync<Permission>("spGetUserPermissions", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<PageList<UserResponse>> GetUsersWithRoleByQueryAsync(UserQueryParameters queryParameters)
        {
            var parameters = new DynamicParameters();
            parameters.Add("pageNumber", queryParameters.PageNo, DbType.Int32, ParameterDirection.Input);
            parameters.Add("pageSize", queryParameters.PageSize, DbType.Int32, ParameterDirection.Input);

            using var connection = _dapperDataContext.Connection;

            var records = await connection.QueryAsync<UserResponse>("spGetUserRecords", parameters, commandType: CommandType.StoredProcedure);

            if (!string.IsNullOrEmpty(queryParameters.Email))
                records = records.Where(e => e.Email.ToLowerInvariant().Contains(queryParameters.Email.ToLowerInvariant()));

            var pagedRecords = PageList<UserResponse>.Create(records, queryParameters.PageNo, queryParameters.PageSize, records != null ? records.First().Total : 0);

            return pagedRecords;
        }
    }
}
