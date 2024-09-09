using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Domain.Entities;
using Vetsus.Persistence.Contexts;

namespace Vetsus.Persistence.Repositories
{
    public sealed class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }
    }
}
