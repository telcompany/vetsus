using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces;
using Vetsus.Application.Interfaces.Persistence;

namespace Vetsus.Application.Features.User.Commands
{
    public record ChangePasswordCommand(ChangePasswordRequest Command) : IRequest<Unit>;

    public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.Users.GetByIdAsync(request.Command.Id);

            if (record is null) throw new NotFoundException($"Usuario con id={request.Command.Id} no registrado");

            if (!BCrypt.Net.BCrypt.Verify(request.Command.CurrentPassword, record.PasswordHash))
            {
                throw new InvalidCredentialException("Contraseña actual inválida");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Command.NewPassword);

            _unitOfWork.BeginTransaction();

            record.PasswordHash = passwordHash;

            await _unitOfWork.Users.UpdateAsync(record);
            _unitOfWork.CommitAndCloseConnection();

            return Unit.Value;
        }
    }
}
