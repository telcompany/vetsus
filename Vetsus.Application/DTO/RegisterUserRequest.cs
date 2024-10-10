namespace Vetsus.Application.DTO
{
    public record RegisterUserRequest(string UserName, string FirstName, string LastName, string Email, string Role, string Password);
}
