using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;

namespace Vetsus.Application.Features.User.Commands
{
    public record RegisterUserCommand(RegisterUserRequest Command) : IRequest<Unit>;

    public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Command.Password);

            _unitOfWork.BeginTransaction();
            var userId = await _unitOfWork.Users.AddAsync(new Domain.Entities.User
            {
                Email = request.Command.Email,
                UserName = request.Command.UserName,
                PasswordHash = passwordHash
            });
            _unitOfWork.CommitAndCloseConnection();

            return Unit.Value;
        }
    }
}
