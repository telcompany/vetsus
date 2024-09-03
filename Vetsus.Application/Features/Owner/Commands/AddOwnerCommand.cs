using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Wrappers;

namespace Vetsus.Application.Features.Owner.Commands
{
    public record AddOwnerCommand(CreateOwnerRequest CreateOwnerRequest) : IRequest<Response<string>>;

    public sealed class AddOwnerCommandHandler : IRequestHandler<AddOwnerCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddOwnerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<string>> Handle(AddOwnerCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            var ownerId = await _unitOfWork.Owners.AddAsync(new Domain.Entities.Owner
            {
                FirstName = request.CreateOwnerRequest.FirstName,
                LastName = request.CreateOwnerRequest.LastName,
                Address = request.CreateOwnerRequest.Address,
                Phone = request.CreateOwnerRequest.Phone,
                Email = request.CreateOwnerRequest.Email
            });
            _unitOfWork.CommitAndCloseConnection();

            return new Response<string>(ownerId);
        }
    }
}
