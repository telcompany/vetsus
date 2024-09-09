using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Wrappers;

namespace Vetsus.Application.Features.User.Commands
{
    public record RegisterUserCommand(RegisterUserRequest Command) : IRequest<Response<string>>;

    public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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

            return new Response<string>(userId, null);
        }
    }
}
