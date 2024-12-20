using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Wrappers;

namespace Vetsus.Application.Features.User.Queries
{
    public record GetUserByIdQuery(string Id) : IRequest<Response<UserByIdResponse>>;

    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<UserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.Users.GetUserByIdQueryAsync(request.Id);

            if (record == null) throw new NotFoundException($"Usuario con id={request.Id} no registrado");

            var response = new UserByIdResponse(record.Id, record.FirstName, record.LastName, record.Email, record.Username, record.Role);

            return new Response<UserByIdResponse>(response);
        }
    }
}
