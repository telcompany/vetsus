using Vetsus.Application.DTO;
using Vetsus.Domain.Entities;

namespace Vetsus.Application.Interfaces.Persistence
{
    public interface IPetRepository: IGenericRepository<Pet>
    {
        public Task<IEnumerable<Pet>> GetPetsByOwnerId(string ownerId);
    }
}
