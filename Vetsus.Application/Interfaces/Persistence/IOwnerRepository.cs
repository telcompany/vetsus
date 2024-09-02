using Vetsus.Application.DTO;
using Vetsus.Application.Utilities;
using Vetsus.Domain.Entities;
using Vetsus.Domain.QueryParameters;

namespace Vetsus.Application.Interfaces.Persistence
{
    public interface IOwnerRepository: IGenericRepository<Owner>
    {
        public Task<PageList<OwnerResponse>> GetOwnersByQueryAsync(OwnerQueryParameters queryParameters);
    }
}
