using Vetsus.Application.DTO;
using Vetsus.Application.Utilities;
using Vetsus.Domain.Entities;
using Vetsus.Domain.QueryParameters;

namespace Vetsus.Application.Interfaces.Persistence
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetUserByEmail(string email);
        public Task<IEnumerable<Permission>> GetUserPermissions(User user);
        public Task<PageList<UserResponse>> GetUsersByQueryAsync(UserQueryParameters queryParameters);
        public Task<PageList<UserResponse>> GetUsersWithRoleByQueryAsync(UserQueryParameters queryParameters);
        public Task AddUserRole(string roleId, string userId);
        public Task<UserByIdResponse> GetUserByIdQueryAsync(string id);
    }
}
