using Vetsus.Application.DTO;
using Vetsus.Application.Utilities;
using Vetsus.Domain.Entities;
using Vetsus.Domain.QueryParameters;

namespace Vetsus.Application.Interfaces.Persistence
{
    public interface ISpeciesRepository: IGenericRepository<Species>
    {
        public Task<PageList<SpeciesResponse>> GetSpeciesByQueryAsync(SpeciesQueryParameters queryParameters);
    }
}
