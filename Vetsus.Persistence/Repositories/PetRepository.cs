using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Domain.Entities;
using Vetsus.Persistence.Contexts;

namespace Vetsus.Persistence.Repositories
{
    public sealed class PetRepository: GenericRepository<Pet>, IPetRepository
    {
        public PetRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }
    }
}
