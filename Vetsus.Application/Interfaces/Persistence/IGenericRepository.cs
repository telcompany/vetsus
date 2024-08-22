using System.Security.Principal;
using Vetsus.Domain.Common;
using Vetsus.Domain.Utilities;

namespace Vetsus.Application.Interfaces.Persistence
{
    public interface IGenericRepository<T> where T : IDbEntity
    {
        Task<IEnumerable<T>> GetAsync(QueryParameters queryParameters, params string[] selectedData);
        Task<T> GetByIdAsync(string guid, params string[] selectedData);
        Task<string> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task SoftDeleteAsync(string id, bool softDeleteFromRelatedChildTables = false);
        Task<int> GetTotalCountAsync();
    }
}
