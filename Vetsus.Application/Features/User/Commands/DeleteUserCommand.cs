using MediatR;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;

namespace Vetsus.Application.Features.User.Commands
{
    public record DeleteUserCommand(string Id) : IRequest;

    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _unitOfWork.Users.GetByIdAsync(request.Id);

            if (recordToDelete == null) throw new NotFoundException($"Usuario con id={request.Id} no registrado");

            _unitOfWork.BeginTransaction();
            await _unitOfWork.Users.SoftDeleteAsync(request.Id);
            _unitOfWork.CommitAndCloseConnection();
        }
    }
}
