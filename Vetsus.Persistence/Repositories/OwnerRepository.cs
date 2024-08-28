using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Domain.Entities;
using Vetsus.Persistence.Contexts;

namespace Vetsus.Persistence.Repositories
{
    public sealed class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }
    }
}
