using MediatR;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;

namespace Vetsus.Application.Features.Owner.Commands
{
    public record UpdateOwnerCommand(string Id, string FirstName, string LastName, string Address, string Phone, string Email) : IRequest<Unit>;

    public sealed class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateOwnerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            var requiredOwner = await _unitOfWork.Owners.GetByIdAsync(request.Id);

            if (requiredOwner == null)
                throw new NotFoundException($"Owner con id={request.Id} no registrado");

            _unitOfWork.BeginTransaction();
            requiredOwner.FirstName = request.FirstName;
            requiredOwner.LastName = request.LastName;
            requiredOwner.Address = request.Address;
            requiredOwner.Phone = request.Phone;
            requiredOwner.Email = request.Email;
            await _unitOfWork.Owners.UpdateAsync(requiredOwner);
            _unitOfWork.CommitAndCloseConnection();

            return Unit.Value;
        }
    }
}
