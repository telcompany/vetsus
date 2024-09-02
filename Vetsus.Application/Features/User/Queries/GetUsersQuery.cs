using MediatR;
using Vetsus.Application.DTO;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Utilities;
using Vetsus.Application.Wrappers;
using Vetsus.Domain.QueryParameters;

namespace Vetsus.Application.Features.User.Queries
{
    public record GetUsersQuery(UserQueryParameters QueryParameters) : IRequest<Response<PageList<UserResponse>>>;

    public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Response<PageList<UserResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<PageList<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Users.GetUsersByQueryAsync(request.QueryParameters);

            return new Response<PageList<UserResponse>>(result);
        }
    }
}
