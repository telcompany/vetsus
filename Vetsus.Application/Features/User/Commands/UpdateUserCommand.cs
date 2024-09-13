using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;

namespace Vetsus.Application.Features.User.Commands
{
    public record UpdateUserCommand(UpdateUserRequest Command) : IRequest<Unit>;

    public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.Users.GetByIdAsync(request.Command.Id);

            if (record == null) throw new NotFoundException($"Usuario con id={request.Command.Id} no registrado");

            _unitOfWork.BeginTransaction();
            record.UserName = request.Command.Username;

            //TODO: Update Role

            await _unitOfWork.Users.UpdateAsync(record);
            _unitOfWork.CommitAndCloseConnection();

            return Unit.Value;
        }
    }
}
