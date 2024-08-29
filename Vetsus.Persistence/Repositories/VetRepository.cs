using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Domain.Entities;
using Vetsus.Persistence.Contexts;

namespace Vetsus.Persistence.Repositories
{
    public sealed class VetRepository: GenericRepository<Vet>, IVetRepository
    {
        public VetRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }
    }
}
