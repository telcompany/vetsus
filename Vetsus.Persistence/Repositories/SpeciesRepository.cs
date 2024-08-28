using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Domain.Entities;
using Vetsus.Persistence.Contexts;

namespace Vetsus.Persistence.Repositories
{
    public sealed class SpeciesRepository: GenericRepository<Species>, ISpeciesRepository
    {
        public SpeciesRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }
    }
}
