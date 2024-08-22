using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Domain.Entities;
using Vetsus.Persistence.Contexts;

namespace Vetsus.Persistence.Repositories
{
    public sealed class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }
    }
}
