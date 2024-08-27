using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;
using Vetsus.Application.DTO;
using Vetsus.Application.Exceptions;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Application.Wrappers;

namespace Vetsus.Application.Features.User.Queries
{
    public record LoginUserQuery(string Email, string Password) : IRequest<Response<LoginUserResponse>>;

    public sealed class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Response<LoginUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginUserQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Response<LoginUserResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserByEmail(request.Email);

            if (user is null)
            {
                throw new UserNotFoundException($"No hay una cuenta registrada con email={request.Email}");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new InvalidCredentialException("Las credenciales del usuario no son válidas");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            foreach (var permission in await _unitOfWork.Users.GetUserPermissions(user))
                claims.Add(new Claim("permissions", permission.Name));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            var httpContext = _httpContextAccessor.HttpContext;
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            var loginResponse = new LoginUserResponse(user.UserName);

            return new Response<LoginUserResponse>(loginResponse);
        }
    }
}
