using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Utilities;
using Vetsus.Domain.QueryParameters;

namespace Vetsus.Application.Interfaces
{
    public interface ISpeciesService
    {
        public Task<PageList<SpeciesResponse>> GetSpecies();
    }

    public class SpeciesService : ISpeciesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SpeciesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PageList<SpeciesResponse>> GetSpecies()
        {
            return await _unitOfWork.Species.GetSpeciesByQueryAsync(new SpeciesQueryParameters());
        }
    }
}
