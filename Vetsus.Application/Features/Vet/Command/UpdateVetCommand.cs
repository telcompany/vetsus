using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;

namespace Vetsus.Application.Features.Vet.Command
{
    public record UpdateVetCommand(UpdateVetRequest Command) : IRequest<Unit>;

    public sealed class UpdateVetCommandHandler : IRequestHandler<UpdateVetCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateVetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateVetCommand request, CancellationToken cancellationToken)
        {
            var requiredRecord = await _unitOfWork.Vets.GetByIdAsync(request.Command.Id);

            if (requiredRecord == null) throw new NotFoundException($"Doctor con id={request.Command.Id} no registrado");

            _unitOfWork.BeginTransaction();
            requiredRecord.FirstName = request.Command.FirstName;
            requiredRecord.LastName = request.Command.LastName;
            requiredRecord.Phone = request.Command.Phone;
            await _unitOfWork.Vets.UpdateAsync(requiredRecord);
            _unitOfWork.CommitAndCloseConnection();

            return Unit.Value;
        }
    }
}
