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

        public async Task<PageList<UserResponse>> GetUsersByQueryAsync(UserQueryParameters queryParameters)
        {
            var records = (await GetAsync(queryParameters, "Email", "UserName"))
                        .AsQueryable()
                        .Select(e => new UserResponse(e.Email, e.UserName));

            if (!string.IsNullOrEmpty(queryParameters.Email))
                records = records.Where(e => e.Email.ToLowerInvariant().Contains(queryParameters.Email.ToLowerInvariant()));

            if (!string.IsNullOrEmpty(queryParameters.SortBy))
                if (typeof(User).GetProperty(queryParameters.SortBy) != null)
                    records = records.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);


            var pagedRecords = PageList<UserResponse>.Create(records, queryParameters.PageNo, queryParameters.PageSize, 2000000);

            return pagedRecords;
        }
    }
}
