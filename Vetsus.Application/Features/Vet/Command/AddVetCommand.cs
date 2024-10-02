using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Wrappers;

namespace Vetsus.Application.Features.Vet.Command
{
    public record AddVetCommand(CreateVetRequest CreateVetRequest) : IRequest<Response<string>>;

    public sealed class AddVetCommandHandler : IRequestHandler<AddVetCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddVetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<string>> Handle(AddVetCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            var recordId = await _unitOfWork.Vets.AddAsync(new Domain.Entities.Vet
            {
                FirstName = request.CreateVetRequest.FirstName,
                LastName = request.CreateVetRequest.LastName,
                Phone = request.CreateVetRequest.Phone
            });
            _unitOfWork.CommitAndCloseConnection();

            return new Response<string>(recordId, "");
        }
    }
}
