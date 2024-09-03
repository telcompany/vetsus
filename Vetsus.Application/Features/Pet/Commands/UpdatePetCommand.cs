using MediatR;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;

namespace Vetsus.Application.Features.Pet.Commands
{
    public record UpdatePetCommand(string Id, string Name, DateTime BirthDate, string SpeciesId, string OwnerId) : IRequest<Unit>;

    public sealed class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
        {
            var requiredPet = await _unitOfWork.Pets.GetByIdAsync(request.Id);

            if (requiredPet == null)
                throw new NotFoundException($"Pet con id={request.Id} no registrado");

            _unitOfWork.BeginTransaction();
            requiredPet.Name = request.Name;
            requiredPet.BirthDate = request.BirthDate;
            requiredPet.SpeciesId = request.SpeciesId;
            requiredPet.OwnerId = request.OwnerId;
            
            await _unitOfWork.Pets.UpdateAsync(requiredPet);
            _unitOfWork.CommitAndCloseConnection();

            return Unit.Value;
        }
    }
}
