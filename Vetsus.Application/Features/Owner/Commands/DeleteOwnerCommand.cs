using MediatR;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;

namespace Vetsus.Application.Features.Owner.Commands
{
    public record DeleteOwnerCommand(string Id) : IRequest;

    public sealed class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteOwnerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _unitOfWork.Owners.GetByIdAsync(request.Id);

            if (recordToDelete == null) throw new NotFoundException($"Dueño con id={request.Id} no registrado");

            _unitOfWork.BeginTransaction();
            await _unitOfWork.Owners.SoftDeleteAsync(request.Id);
            _unitOfWork.CommitAndCloseConnection();
        }
    }
}
