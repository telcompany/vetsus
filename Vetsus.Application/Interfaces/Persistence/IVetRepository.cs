using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vetsus.Domain.Entities;

namespace Vetsus.Application.Interfaces.Persistence
{
    public interface IVetRepository: IGenericRepository<Vet>
    {
    }
}
