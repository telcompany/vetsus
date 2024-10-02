using MediatR;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;

namespace Vetsus.Application.Features.Vet.Command
{
    public record DeleteVetCommand(string Id) : IRequest;

    public sealed class DeleteVetCommandHandler : IRequestHandler<DeleteVetCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteVetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteVetCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _unitOfWork.Vets.GetByIdAsync(request.Id);

            if (recordToDelete == null) throw new NotFoundException($"Doctor con id={request.Id} no registrado");

            _unitOfWork.BeginTransaction();
            await _unitOfWork.Vets.SoftDeleteAsync(request.Id);
            _unitOfWork.CommitAndCloseConnection();
        }
    }
}
