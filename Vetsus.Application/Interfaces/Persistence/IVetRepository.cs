using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vetsus.Application.DTO;
using Vetsus.Application.Utilities;
using Vetsus.Domain.Entities;
using Vetsus.Domain.QueryParameters;

namespace Vetsus.Application.Interfaces.Persistence
{
    public interface IVetRepository: IGenericRepository<Vet>
    {
        public Task<PageList<VetResponse>> GetVetsByQueryAsync(VetQueryParameters queryParameters);
    }
}
