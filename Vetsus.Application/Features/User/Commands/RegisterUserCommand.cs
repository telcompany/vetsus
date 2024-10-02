using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Wrappers;

namespace Vetsus.Application.Features.User.Commands
{
    public record RegisterUserCommand(RegisterUserRequest Command) : IRequest<Response<string>>;

    public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var role = (await _unitOfWork.Roles.GetAsync(new Domain.Utilities.QueryParameters(), "Id", "Name"))
                .FirstOrDefault(x => x.Name == request.Command.Role);

            if (role == null) throw new NotFoundException($"Rol: {request.Command.Role} no registrado");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Command.Password);

            _unitOfWork.BeginTransaction();

            string userId = await _unitOfWork.Users.AddAsync(new Domain.Entities.User
            {
                Email = request.Command.Email,
                UserName = request.Command.UserName,
                PasswordHash = passwordHash,
                CreatedBy = _userService.User.UserName!
            });

            await _unitOfWork.Users.AddUserRole(role.Id, userId);

            _unitOfWork.CommitAndCloseConnection();

            return new Response<string>(userId, null);
        }
    }
}
