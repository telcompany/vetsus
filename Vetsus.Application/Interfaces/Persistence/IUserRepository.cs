using Vetsus.Domain.Entities;

namespace Vetsus.Application.Interfaces.Persistence
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetUserByEmail(string email);
        public Task<IEnumerable<Permission>> GetUserPermissions(User user);
    }
}
