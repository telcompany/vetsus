using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Vetsus.Application.DTO;

namespace Vetsus.Application.Interfaces
{
	public interface IUserService
	{
		CurrentUser User { get; }
	}

	public class UserService : IUserService
	{
		public CurrentUser User { get; }

		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;

			var username = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Name)?.Value;
			var role = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Role)?.Value;

			User = new CurrentUser(username, role);
		}
	}
}
