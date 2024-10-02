using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;

namespace Vetsus.Application.Features.Owner.Commands
{
    public record UpdateOwnerCommand(UpdateOwnerRequest Command) : IRequest<Unit>;

    public sealed class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateOwnerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            var requiredOwner = await _unitOfWork.Owners.GetByIdAsync(request.Command.Id);

            if (requiredOwner == null) throw new NotFoundException($"Owner con id={request.Command.Id} no registrado");

            _unitOfWork.BeginTransaction();
            requiredOwner.FirstName = request.Command.FirstName;
            requiredOwner.LastName = request.Command.LastName;
            requiredOwner.Address = request.Command.Address;
            requiredOwner.Phone = request.Command.Phone;
            requiredOwner.Email = request.Command.Email;
            await _unitOfWork.Owners.UpdateAsync(requiredOwner);
            _unitOfWork.CommitAndCloseConnection();

            return Unit.Value;
        }
    }
}
